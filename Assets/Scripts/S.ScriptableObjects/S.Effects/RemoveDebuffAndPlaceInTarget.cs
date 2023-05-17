using System.Collections.Generic;
using UnityEngine;

namespace S.ScriptableObjects.S.Effects
{
    [CreateAssetMenu(fileName = "RemoveDebuffAndPlaceInTargetEffect", menuName = "Effects/Create new RemoveDebuffAndPlaceInTargetEffect")]
    public class RemoveDebuffAndPlaceInTarget : ScriptableObjects.Effects
    {
        public override void Apply(Champions user, Champions target)
        {
            Debug.Log("RemoveDebuff & Place in Target");
        }
        public override void Apply(Champions user, List<Champions> target)
        {
            throw new System.NotImplementedException();
        }
    }
}
