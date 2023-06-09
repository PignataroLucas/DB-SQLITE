using System;
using System.Collections;
using System.Collections.Generic;
using S.DataBase;
using S.Structures;
using S.Utility.Event_Manager;
using UnityEngine;

namespace S.Grid
{
    public class StructurePlacer : MonoBehaviour , IEventListener
    {
        [SerializeField] private List<GameObject> structurePrefabs;
        [SerializeField] private Grid grid;
        [SerializeField] private DatabaseManager databaseManager;

        private List<GameObject> _placedStructures;

        private int _currentStructureId;

        private void Awake()
        {
            OnEnableListenerSubscriptions();
        }

        private void Start()
        {
            _placedStructures = new List<GameObject>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceStructureAtMousePosition(_currentStructureId);
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

                    // Cambia el color de las celdas ocupadas a rojo
                    grid.ChangeCellColor(gridIndex.x, gridIndex.y, structureData.CellOccupiedX, structureData.CellOccupiedY, Color.red);
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
                    return prefab;
                }
            }
            return null;
        }

        private void BuyStructure(Hashtable obj)
        {
            if (obj != null && obj.ContainsKey("structureId"))
            {
                _currentStructureId = (int)obj["structureId"];
            }
        }
        public void OnEnableListenerSubscriptions()
        {
            EventManager.StartListening(GenericEvents.BuyStructure,BuyStructure);
        }
        public void OnDisableListenerSubscriptions()
        {
            EventManager.StopListening(GenericEvents.BuyStructure,BuyStructure);
        }
        
    }
    
}
