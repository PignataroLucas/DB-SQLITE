using UnityEngine;

namespace S.Grid
{
    public class Grid : MonoBehaviour
    {
        public GameObject gridPlane;
        public float cellSize;

        private int _width;
        private int _height;
        private Vector3[,] _grid;

        
        public int Width => _width;
        public int Height => _height;
        
        public float offsetX = 0f;
        public float offsetZ = 0f;
        
        

        private void Start()
        {
            CalculateGridDimensions();
            _grid = new Vector3[_width, _height];
            CreateGrid();
        }

        private void CalculateGridDimensions()
        {
            var localScale = gridPlane.transform.localScale;
            float planeWidth = localScale.x * 10;
            float planeHeight = localScale.z * 10;

            _width = Mathf.FloorToInt(planeWidth / cellSize);
            _height = Mathf.FloorToInt(planeHeight / cellSize);
        }

        private void CreateGrid()
        {
            
            _width = Mathf.RoundToInt(PlaneSize.x / cellSize);
            _height = Mathf.RoundToInt(PlaneSize.y / cellSize);
            _grid = new Vector3[_width, _height];

            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _height; z++)
                {
                    float xPos = transform.position.x - PlaneSize.x * 0.5f + x * cellSize + cellSize * 0.5f + offsetX;
                    float zPos = transform.position.z - PlaneSize.y * 0.5f + z * cellSize + cellSize * 0.5f + offsetZ;
                    _grid[x, z] = new Vector3(xPos, 0, zPos);
                }
            }
        }

        public Vector3 GetCellPosition(int x, int z)
        {
            Vector3 planePosition = gridPlane.transform.position;
            return new Vector3(x * cellSize + planePosition.x - (PlaneSize.x * 0.5f) + (cellSize * 0.5f) + offsetX, 0, z * cellSize + planePosition.z - (PlaneSize.y * 0.5f) + (cellSize * 0.5f) + offsetZ);
        }


        public Vector3[,] GetGrid()
        {
            return _grid;
        }

        private void OnDrawGizmos()
        {
            //if (gridPlane == null) return;

            CalculateGridDimensions();
            _grid ??= new Vector3[_width, _height];
            CreateGrid();

            Gizmos.color = Color.yellow;

            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _height; z++)
                {
                    Vector3 cellPosition = GetCellPosition(x, z);
                    Gizmos.DrawWireCube(cellPosition, new Vector3(cellSize, 0.1f, cellSize));
                }
            }
        }
        public Vector2 PlaneSize
        {
            get
            {
                MeshFilter meshFilter = gridPlane.GetComponent<MeshFilter>();
                if (meshFilter == null) return Vector2.zero;

                Vector3 meshSize = meshFilter.sharedMesh.bounds.size;
                Vector3 localScale = gridPlane.transform.localScale;
                return new Vector2(meshSize.x * localScale.x, meshSize.z * localScale.z);
            }
        }
        public Vector2Int GetGridIndex(Vector3 worldPosition)
        {
            Vector3 localPosition = worldPosition - gridPlane.transform.position + new Vector3(PlaneSize.x * 0.5f, 0, PlaneSize.y * 0.5f) + new Vector3(offsetX, 0, offsetZ);
            int x = Mathf.FloorToInt(localPosition.x / cellSize);
            int z = Mathf.FloorToInt(localPosition.z / cellSize);

            return new Vector2Int(x, z);
        }
    }
}
