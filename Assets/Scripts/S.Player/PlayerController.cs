using System;
using S.DataBase;
using UnityEngine;
using UnityEngine.Serialization;

namespace S.Player
{
    public class PlayerController : MonoBehaviour
    {
        public int id;
        private string _class;
        private int _life;
        private int _speedMovement;
        private int _attack;

        private DatabaseManager _databaseManager;

        private void Awake()
        {
            _databaseManager = FindObjectOfType<DatabaseManager>();
        }
        private void Start()
        {
            
            LoadPlayerStats(id);
        }

        private void LoadPlayerStats(int playerId)
        {
            PlayerStats playerStats = _databaseManager.GetPlayerStats(playerId);
        
            if (playerStats != null)
            {
                _class = playerStats.Class;
                _life = playerStats.Life;
                _speedMovement = playerStats.SpeedMovement;
                _attack = playerStats.Attack;
            }
            else
            {
                Debug.LogError($"Player stats for ID {playerId} not found");
            }
            
            Debug.Log($"ID : {id} , Class : {_class} , Life : {_life} , Speed Movement : {_speedMovement} , " +
                      $"Damage : {_attack}");
            
        }

        public void UpdateSpeed (int newSpeed)
        {
            _speedMovement = newSpeed;
            _databaseManager.UpdatePlayerStats(id,_class,_life,_speedMovement,_attack);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                UpdateSpeed(_speedMovement + 1);
            }
        }
    }
}
