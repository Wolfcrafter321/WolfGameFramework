using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [System.Serializable]
    public class WolfEventSO : ScriptableObject
    {
        public List<WolfEventBase> wolfEvents;

        [HideInInspector]
        public WolfEventBase nextEvent;

        public void StartEvent(WolfEventManager manager)
        {
            nextEvent = wolfEvents[0];
            manager.StartCoroutine(StartEventCoroutine());
        }

        IEnumerator StartEventCoroutine()
        {
            while (nextEvent != null)
            {
                yield return nextEvent.ProcessEvent(this);
            }
        }

    }
}