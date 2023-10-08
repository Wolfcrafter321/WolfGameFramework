using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [System.Serializable]
    public class WolfEventPrint : WolfEventBase
    {
        public int count = 3;

        public override IEnumerator ProcessEvent(WolfEventSO source)
        {
            for (int i = 0; i < count; i++)
            {
                Debug.Log(base.text);
                yield return null;
            }

            if (targetEvent == -1)
                source.nextEvent = null;
            else
                source.nextEvent = source.wolfEvents[targetEvent];
            yield return null;
            // yield return base.ProcessEvent(source);

        }
    }
}