using System;
using System.Collections.Generic;
using S.DataBase;
using S.Structures;
using UnityEngine;

namespace S.Grid
{
    public class StructurePlacer : MonoBehaviour
    {
        [SerializeField] private GameObject cube;
        [SerializeField] private Grid grid;
        [SerializeField] private DatabaseManager databaseManager;

        private List<GameObject> _placedStructures;

        private void Start()
        {
            _placedStructures = new List<GameObject>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceStructureAtMousePosition();
            }
        }

        private void PlaceStructureAtMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 gridPosition = grid.transform.InverseTransformPoint(hit.point);
                Vector2Int gridIndex = grid.GetGridIndex(gridPosition);
                
                if (IsCellOccupied(gridIndex))
                {
                    return;
                }

                
                StructureData structureData = databaseManager.GetStructureData(1);

                if (structureData != null)
                {
                    Vector3 position = grid.GetCellPosition(gridIndex.x, gridIndex.y);

                    GameObject newStructure = Instantiate(cube, position, Quaternion.identity);
                    newStructure.GetComponent<Structure>().LoadStructureStats(structureData.Id);
                    _placedStructures.Add(newStructure);
                }
            }
        }
        
        private bool IsCellOccupied(Vector2Int gridIndex)
        {
            foreach (GameObject structure in _placedStructures)
            {
                Structure structureComponent = structure.GetComponent<Structure>();
                Vector2Int structureGridIndex = grid.GetGridIndex(structure.transform.position);

                if (gridIndex == structureGridIndex)
                {
                    return true;
                }
            }

            return false;
        }
        
        
    }
    
}
