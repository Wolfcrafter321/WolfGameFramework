using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [System.Serializable]
    public class WolfEventBase
    {

        public int targetEvent;

        [Header("node")]
        public float nodeX = 0;
        public float nodeY = 0;

        public string text;

        public virtual IEnumerator ProcessEvent(WolfEventSO source)
        {
            Debug.Log("これはデバッグテキストです。：" + text);

            // throw new System.NotImplementedException();
            if(targetEvent == -1)
                source.nextEvent = null;
            else
                source.nextEvent = source.wolfEvents[targetEvent];
            yield return null;
        }

    }
}