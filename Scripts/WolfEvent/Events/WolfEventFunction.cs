using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wolf
{
    [FunctionNode]
    public class WolfEventFunction : WolfEventBase
    {
        public enum EventFunctionType { Start = 0, Update = 1, Custom = 20 }
        public EventFunctionType eventType;

        public static new string searchTreePath = "Start";

        public override IEnumerator ProcessEvent(WolfEventSO source)
        {
            return base.ProcessEvent(source);
        }

    }
}