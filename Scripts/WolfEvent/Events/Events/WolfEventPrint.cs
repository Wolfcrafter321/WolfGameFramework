using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [System.Serializable, Node]
    public class WolfEventPrint : WolfEventBase
    {
        [NodeField]public int count = 3;
        [NodeField]public string text;
        [NodeConnectableField] public WolfEventConnectableVariable<string> text_test;

        public static new string searchTreePath = "Debug/Print";

        public override IEnumerator ProcessEvent(WolfEventSO source)
        {
            for (int i = 0; i < count; i++)
            {
                Debug.Log(text_test.value);
                yield return null;
            }

            if (targetEvent == -1)
                source.nextEvent = null;
            else
                source.nextEvent = source.wolfEvents[targetEvent];
            yield return null;
            yield return base.ProcessEvent(source);
        }

    }
}