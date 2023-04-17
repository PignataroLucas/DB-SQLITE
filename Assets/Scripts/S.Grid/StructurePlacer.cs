using System;
using System.Collections.Generic;
using S.DataBase;
using S.Structures;
using UnityEngine;

namespace S.Grid
{
    public class StructurePlacer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> structurePrefabs;
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
                int structureId = 2;
                PlaceStructureAtMousePosition(structureId);
            }
        }

        private void PlaceStructureAtMousePosition(int structureId )
        {
            StructureData structureData = databaseManager.GetStructureData(structureId);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 gridPosition = grid.transform.InverseTransformPoint(hit.point);
                Vector2Int gridIndex = grid.GetGridIndex(gridPosition);

                
                if (IsCellOccupied(gridIndex, structureData.CellOccupiedX, structureData.CellOccupiedY))
                {
                    return;
                }

                Vector3 position = grid.GetCellPosition(gridIndex.x, gridIndex.y);

                GameObject structurePrefab = GetPrefabById(structureId);
                if (structurePrefab != null)
                {
                    GameObject newStructure = Instantiate(structurePrefab, position, Quaternion.identity);
                    newStructure.GetComponent<Structure>().LoadStructureStats(structureData.Id);
                    _placedStructures.Add(newStructure);
                }
            }
        }
        
        private bool IsCellOccupied(Vector2Int gridIndex, int cellOccupiedX, int cellOccupiedY)
        {
            for (int x = gridIndex.x; x < gridIndex.x + cellOccupiedX; x++)
            {
                for (int y = gridIndex.y; y < gridIndex.y + cellOccupiedY; y++)
                {
                    Vector2Int currentGridIndex = new Vector2Int(x, y);

                    foreach (GameObject structure in _placedStructures)
                    {
                        Structure structureComponent = structure.GetComponent<Structure>();
                        Vector2Int structureGridIndex = grid.GetGridIndex(structure.transform.position);

                        int structureCellOccupiedX = structureComponent.CellOccupiedX;
                        int structureCellOccupiedY = structureComponent.CellOccupiedY;

                        for (int sx = structureGridIndex.x; sx < structureGridIndex.x + structureCellOccupiedX; sx++)
                        {
                            for (int sy = structureGridIndex.y; sy < structureGridIndex.y + structureCellOccupiedY; sy++)
                            {
                                if (currentGridIndex == new Vector2Int(sx, sy))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        
        private GameObject GetPrefabById(int id)
        {
            foreach (GameObject prefab in structurePrefabs )
            {
                Structure structure = prefab.GetComponent<Structure>();
                if (structure != null && structure.Id == id)
                {
                    Debug.Log(structure.Id);
                    return prefab;
                }
            }
            return null;
        }
        
        
    }
    
}
