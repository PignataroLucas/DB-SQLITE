using System.Collections.Generic;
using UnityEngine;

namespace S.ScriptableObjects.S.Effects
{
    [CreateAssetMenu(fileName = "AttackAllEnemiesEffect", menuName = "Effects/Create new AttackAllEnemiesEffect")]
    public class AttackAllEnemies : ScriptableObjects.Effects
    {

        public override void Apply(Champions user, Champions target)
        {
            throw new System.NotImplementedException();
        }

        public override void Apply(Champions user, List<Champions> target)
        {
            Debug.Log("Attack to all Enemies");
        }
    }
}
