using System;
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
                    float xPos = transform.position.x - PlaneSize.x * 0.5f + x * cellSize + cellSize * 0.5f;
                    float zPos = transform.position.z - PlaneSize.y * 0.5f + z * cellSize + cellSize * 0.5f;
                    _grid[x, z] = new Vector3(xPos, 0, zPos);
                }
            }
        }

        public Vector3 GetCellPosition(int x, int z)
        {
            Vector3 planePosition = gridPlane.transform.position;
            return new Vector3(x * cellSize + planePosition.x - (gridPlane.transform.localScale.x * 5), 0, z * cellSize + planePosition.z - (gridPlane.transform.localScale.z * 5));
        }

        public Vector3[,] GetGrid()
        {
            return _grid;
        }

        private void OnDrawGizmos()
        {
            //if (gridPlane == null) return;

            CalculateGridDimensions();
            if (_grid == null) _grid = new Vector3[_width, _height];
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
                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
                var bounds = meshRenderer.bounds;
                return new Vector2(bounds.size.x, bounds.size.z);
            }
        }

    }
}
