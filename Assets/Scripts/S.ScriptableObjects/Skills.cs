using System.Collections.Generic;
using UnityEngine;

namespace S.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Skills",menuName = "Skills/Create new Skill")]
    public class Skills : ScriptableObject
    {
        [SerializeField] private new string name;
        [SerializeField] private int id;
        [TextArea]
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private List<Effects> effects;

        public string Name => name;
        public int Id => id;
        public string Description => description;
        public Sprite Icon => icon;
        public List<Effects> Effects => effects;

    }
    
}
