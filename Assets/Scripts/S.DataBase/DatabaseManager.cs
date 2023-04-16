using System.Data;
using Mono.Data.Sqlite;
using S.Player;
using S.Structures;
using UnityEngine;

namespace S.DataBase
{
    public class DatabaseManager : MonoBehaviour
    {
        private string _db;

        private void Awake()
        {
            _db = "URI=file:" + Application.dataPath + "/Resources/DB/FirstGame.db";
        }
        private void OpenConnection()
        {
            IDbConnection dbConnection = new SqliteConnection(_db);
            dbConnection.Open();

            if (dbConnection.State == ConnectionState.Open)
            {
                Debug.Log(dbConnection.State == ConnectionState.Open ? "Successful connection" : "Connection failed");
            }
            dbConnection.Close();
        }
    
        public PlayerStats GetPlayerStats(int playerId)
        {
            Debug.Log("Database path: " + _db);
            
            IDbConnection dbConnection = new SqliteConnection(_db);
            dbConnection.Open();

            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = "SELECT * FROM PlayerStats";
            IDataReader reader = dbCommand.ExecuteReader();

            PlayerStats playerStats = new PlayerStats();
            
            while (reader.Read())
            {
                playerStats = new PlayerStats
                {
                    Id = reader.GetInt32(0),
                    Class = !reader.IsDBNull(1) ? reader.GetString(1) : "N/A",
                    Life = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                    SpeedMovement = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                    Attack = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
                    Description = !reader.IsDBNull(5) ? reader.GetString(5) : "N/A"
                };
            }
            
            reader.Close();
            dbCommand.Dispose();
            dbConnection.Close();

            return playerStats;
        }

        public StructureData GetStructureData(int structureId)
        {
            IDbConnection dbConnection = new SqliteConnection(_db);
            dbConnection.Open();

            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = "SELECT * FROM Structure";
            IDataReader reader = dbCommand.ExecuteReader();

            StructureData structureData = new StructureData();

            while (reader.Read())
            {
                structureData = new StructureData
                {
                    Id = reader.GetInt32(0),
                    Description = !reader.IsDBNull(1)? reader.GetString(1):"N/A",
                    CellOccupiedX = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                    CellOccupiedY = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                };
            }
            reader.Close();
            dbCommand.Dispose();
            dbConnection.Close();

            return structureData;
        }

        public void UpdatePlayerStats(int id, string playerClass, int life, int speedMovement, int damage)
        {
            IDbConnection dbConnection = new SqliteConnection(_db);
            dbConnection.Open();

            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText =
                "UPDATE PlayerStats SET class = @class, life = @life, speedMovement = @speedMovement, damage = @damage WHERE id = @id";
            
            dbCommand.Parameters.Add(new SqliteParameter("@id", id));
            dbCommand.Parameters.Add(new SqliteParameter("@class", playerClass));
            dbCommand.Parameters.Add(new SqliteParameter("@life", life));
            dbCommand.Parameters.Add(new SqliteParameter("@speedMovement", speedMovement));
            dbCommand.Parameters.Add(new SqliteParameter("@damage", damage));
            //dbCommand.Parameters.Add(new SqliteParameter("@description", description));
            
            
            dbCommand.ExecuteNonQuery();

            dbCommand.Dispose();
            dbConnection.Close();
            
        }

        
    }
}
