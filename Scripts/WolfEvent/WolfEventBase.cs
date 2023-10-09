using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [System.Serializable]
    public class WolfEventBase : ScriptableObject
    {

        public int targetEvent = -1;

        [Header("node")]
        public float nodeX = 0;
        public float nodeY = 0;

        public virtual IEnumerator ProcessEvent(WolfEventSO source)
        {
            Debug.Log("hi this is event. next is " + targetEvent);
            // throw new System.NotImplementedException();
            if(targetEvent == -1)
                source.nextEvent = null;
            else
                source.nextEvent = source.wolfEvents[targetEvent];
            yield return null;
        }

    }
}