using S.Structures;
using UnityEngine;

namespace S.Grid
{
    public class StructurePlacer : MonoBehaviour
    { 
     
        public GameObject structurePrefab;
        public LayerMask structureLayer;
        public Grid gridManager;

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 1.0f);

                if (Physics.Raycast(ray, out hit))
                {
                    int x, z;
                    GetGridCoordinates(hit.point, out x, out z);

                    if (x >= 0 && x < gridManager.Width && z >= 0 && z < gridManager.Height)
                    {
                        Debug.Log("Click dentro de los límites de la grilla");
                        PlaceStructure(hit.point);
                    }
                    else
                    {
                        Debug.Log("Click fuera de los límites de la grilla");
                    }
                }
                else
                {
                    Debug.Log("No se detectó el plano");
                }
            }
        }

        public void PlaceStructure(Vector3 position)
        {
           
            int x, z;
            GetGridCoordinates(position, out x, out z);

            Structure structure = structurePrefab.GetComponent<Structure>();

            if (CanPlaceStructure(x, z, structure.width, structure.height))
            {
                Debug.Log("Colocando estructura");
                Vector3 placementPosition = gridManager.GetGrid()[x, z];
                Instantiate(structurePrefab, placementPosition, Quaternion.identity);
            }
            else
            {
                Debug.Log("No se puede colocar la estructura");
            }
        }

        private void GetGridCoordinates(Vector3 position, out int x, out int z)
        {
            Vector3 gridOrigin = gridManager.transform.position;
            float cellSize = gridManager.cellSize;
            float planeWidth = gridManager.PlaneSize.x;
            float planeLength = gridManager.PlaneSize.y;

            x = Mathf.FloorToInt((position.x - gridOrigin.x + planeWidth * 0.5f) / cellSize);
            z = Mathf.FloorToInt((position.z - gridOrigin.z + planeLength * 0.5f) / cellSize);
        
        }

        private bool CanPlaceStructure(int startX, int startZ, int structureWidth, int structureHeight)
        {
            Vector3[,] grid = gridManager.GetGrid();

            if (startX + structureWidth > gridManager.Width || startZ + structureHeight > gridManager.Height)
            {
                return false;
            }

            for (int x = startX; x < startX + structureWidth; x++)
            {
                for (int z = startZ; z < startZ + structureHeight; z++)
                {
                    Vector3 cellPosition = grid[x, z];
                    if (IsCellOccupied(cellPosition))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsCellOccupied(Vector3 position)
        {
            Collider[] colliders = Physics.OverlapBox(position, new Vector3(gridManager.cellSize / 2, 0.1f, gridManager.cellSize / 2), Quaternion.identity, structureLayer);
            return colliders.Length > 0;
        }
        
       
        
        
    }
}
