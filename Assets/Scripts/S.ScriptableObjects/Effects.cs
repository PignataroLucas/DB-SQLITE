using System.Collections.Generic;
using S.ScriptableObjects;
using UnityEngine;

namespace S.ScriptableObjects
{
    public abstract class Effects : ScriptableObject
    {
        [SerializeField] private new string name;
        public abstract void Apply(Champions user, Champions target);
        public abstract void Apply(Champions user, List<Champions> target);
    }
}

