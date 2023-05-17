using System.Collections.Generic;
using UnityEngine;

namespace S.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AttackOneEnemyEffect", menuName = "Effects/Create new AttackOneEnemyEffect")]
    public class AttackOneEnemy : Effects
    {
        public override void Apply(Champions user, Champions target)
        {
            Debug.Log("Attacking one enemy");
        }

        public override void Apply(Champions user, List<Champions> target)
        {
            throw new System.NotImplementedException();
        }
    }
}
