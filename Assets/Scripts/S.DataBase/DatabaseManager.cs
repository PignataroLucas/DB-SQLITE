using System;
using System.Data;
using Mono.Data.Sqlite;
using S.Player;
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

        private void Start()
        {
            //OpenConnection();
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
                
                
                // int id = reader.GetInt32(0);
                // string playerClass = !reader.IsDBNull(1) ? reader.GetString(1) : "N/A";
                // int life = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0;
                // int speedMovement = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                // int damage = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0;
                // string description = !reader.IsDBNull(5) ? reader.GetString(5) : "N/A";
                //
                // Debug.Log($"ID: {id}, Class: {playerClass}, Life: {life}, SpeedMovement: {speedMovement}," +
                //           $" Damage: {damage}, Description: {description}");
            }
            
            reader.Close();
            dbCommand.Dispose();
            dbConnection.Close();

            return playerStats;
        }
    
    }
}
