using System.Collections.Generic;
using UnityEngine;

namespace S.ScriptableObjects.S.Effects
{
    [CreateAssetMenu(fileName = "DealsExtraDamageEffect", menuName = "Effects/Create new DealsExtraDamageEffect")]
    public class DealsExtraDamage : ScriptableObjects.Effects
    {
        [SerializeField] private float extraDamageMultiplier = 1.5f; // Extra damage multiplier
        
        public override void Apply(Champions user, Champions target)
        {
            Debug.Log("Deals Extra Damage");
        }
        public override void Apply(Champions user, List<Champions> target)
        {
            throw new System.NotImplementedException();
        }
    }
}
