using System.Collections.Generic;
using S.ScriptableObjects;
using UnityEngine;

namespace S.ScriptableObjects
{
    public abstract class Effects : ScriptableObject
    {
        [SerializeField] private new string name;
        [TextArea]
        [SerializeField] private string description;
        public abstract void Apply(Champions user, Champions target);
        public abstract void Apply(Champions user, List<Champions> target);
    }
    
    
    [CreateAssetMenu(fileName = "AttackOneEnemyEffect", menuName = "Effects/Create new AttackOneEnemyEffect")]
    public class AttackOneEnemyEffect : Effects
    {
        public override void Apply(Champions user, Champions target)
        {
            Debug.Log("Attack one Enemy");
        }
        public override void Apply(Champions user, List<Champions> targets)
        {
            // No se aplica a múltiples objetivos
        }
    }
    [CreateAssetMenu(fileName = "RemoveDebuffAndPlaceInTargetEffect", menuName = "Effects/Create new RemoveDebuffAndPlaceInTargetEffect")]
    public class RemoveDebuffAndPlaceInTargetEffect : Effects
    {
        public override void Apply(Champions user, Champions target)
        {
            Debug.Log("Remove Debuff And Place in Target");
        }
        
        public override void Apply(Champions user, List<Champions> targets)
        {
            // No se aplica a múltiples objetivos
        }
    }
    
    [CreateAssetMenu(fileName = "AttackAllEnemiesEffect", menuName = "Effects/Create new AttackAllEnemiesEffect")]
    public class AttackAllEnemiesEffect : Effects
    {
        public override void Apply(Champions user, Champions target)
        {
            Debug.Log("Remove Debuff And Place in Target");
        }
        public override void Apply(Champions user, List<Champions> targets)
        {
            Debug.Log("Attack All enemies");
        }
        
    }
    
    [CreateAssetMenu(fileName = "DealsExtraDamageEffect", menuName = "Effects/Create new DealsExtraDamageEffect")]
    public class DealsExtraDamageEffect : Effects
    {
        [SerializeField] private float extraDamageMultiplier = 1.5f; 

        public override void Apply(Champions user, Champions target)
        {
           Debug.Log("Deals Extra Damage");
        }
        public override void Apply(Champions user, List<Champions> targets)
        {
            Debug.Log("Attack All enemies");
        }
    }
    
}


