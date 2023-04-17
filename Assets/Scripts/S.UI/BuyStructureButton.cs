using S.Utility.Event_Manager;
using UnityEngine;

namespace S.UI
{
    
    public class BuyStructureButton : MonoBehaviour
    {

        [SerializeField] private int structureId;

        public void BuyStructure()
        {
            EventTriggers.TriggerEvent<int>(GenericEvents.BuyStructure, "structureId", structureId);
        }
        
    }
    
    
}
