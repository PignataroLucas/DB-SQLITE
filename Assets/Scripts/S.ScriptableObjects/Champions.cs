using System.Collections.Generic;
using UnityEngine;

namespace S.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Champion",menuName = "Champion/Create new Champion")]
    public class Champions : ScriptableObject
    {
        [SerializeField] private new string name;
        [TextArea]
        [SerializeField] private string description;
        [SerializeField] private Elements element;
        [SerializeField] private int hp;
        [SerializeField] private int attack;
        [SerializeField] private int defense;
        [SerializeField] private int velocity;
        [SerializeField] private int criticProbability;
        [SerializeField] private int criticDamage;
        [SerializeField] private int resistance;
        [SerializeField] private int accuracy;
        [SerializeField] private int power;
        [SerializeField] private List<Skills> skills;

        public string Name => name;
        public string Description => description;
        public Elements Element => element;
        public int Hp => hp;
        public int Attack => attack;
        public int Defense => defense;
        public int Velocity => velocity;
        public int CriticalProbability => criticProbability;
        public int CriticalDamage => criticDamage;
        public int Resistance => resistance;
        public int Accuracy => accuracy;
        public int Power => power;
        public List<Skills> Skills => skills;
        
    }
    public enum Elements
    {
        Magic,
        Spirit,
        Force,
        Void
    }
}
