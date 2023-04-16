using UnityEngine;
using S.DataBase;

namespace S.Structures
{
    public class Structure : MonoBehaviour
    {
        [SerializeField] private int id;
        private string _description;
        private int _cellOccupiedX;
        private int _cellOccupiedY;

        public int Id => id;
        public string Description => _description;
        public int CellOccupiedX => _cellOccupiedX;
        public int CellOccupiedY => _cellOccupiedY;

        private DatabaseManager _databaseManager;
        
        private void Awake()
        {
            _databaseManager = FindObjectOfType<DatabaseManager>();
        }

        private void Start()
        {
            LoadStructureStats(id);
        }

        public void LoadStructureStats(int id)
        {
            StructureData structureData = _databaseManager.GetStructureData(this.id);

            if (structureData == null) return;
            this.id = structureData.Id;
            _description = structureData.Description;
            _cellOccupiedX = structureData.CellOccupiedX;
            _cellOccupiedY = structureData.CellOccupiedY;

            Debug.Log(
                $"ID : {id} , Description : {_description} , CellsX : {_cellOccupiedX} , CellsY : {_cellOccupiedY} "); 
        }
    }
}
