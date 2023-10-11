using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [Node]
    public class WolfEventDebugLog : WolfEventBase
    {
        public static new string searchTreePath = "Debug/DebugLog";

        [NodeField]public string text;

        public override IEnumerator ProcessEvent(WolfEventSO source)
        {
            Debug.Log(text);
            return base.ProcessEvent(source);
        }
    }
}