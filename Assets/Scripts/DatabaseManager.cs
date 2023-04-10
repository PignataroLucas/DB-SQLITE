using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class DatabaseManager : MonoBehaviour
{
    private string _db;

    private void Start()
    {
        _db = "URI=file:" + Application.dataPath + "/Resources/DB/FirstGame.db";
        Debug.Log("Database path: " + _db);
        OpenConnection();
        PrintPlayerStats();
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
    
    private void PrintPlayerStats()
    {
        IDbConnection dbConnection = new SqliteConnection(_db);
        dbConnection.Open();

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM PlayerStats";
        IDataReader reader = dbCommand.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string playerClass = !reader.IsDBNull(1) ? reader.GetString(1) : "N/A";
            int life = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0;
            int speedMovement = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
            int damage = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0;
            string description = !reader.IsDBNull(5) ? reader.GetString(5) : "N/A";
            Debug.Log($"ID: {id}, Class: {playerClass}, Life: {life}, SpeedMovement: {speedMovement}, " +
                                                                 $"Damage: {damage}, Description: {description}");
        }
        
        reader.Close();
        dbCommand.Dispose();
        dbConnection.Close();
    }
    
}
