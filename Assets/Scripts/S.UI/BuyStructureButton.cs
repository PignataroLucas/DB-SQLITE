using S.Utility.Event_Manager;
using UnityEngine;

namespace S.UI
{
    
    public class BuyStructureButton : MonoBehaviour
    {

        public void BuyStructure()
        {
            EventManager.TriggerEvent(GenericEvents.BuyStructure);
        }
        
    }
    
    
}
