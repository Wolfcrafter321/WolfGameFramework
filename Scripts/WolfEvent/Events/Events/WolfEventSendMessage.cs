using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [Node]
    public class WolfEventSendMessage : WolfEventBase
    {
        [NodeField]public GameObject target;
        [NodeConnectableField] public WolfEventConnectableVariable<string> message;

        public static new string searchTreePath = "Unity/SendMessage";

        public override IEnumerator ProcessEvent(WolfEventSO source)
        {
            target.SendMessage(message.value);
            return base.ProcessEvent(source);
        }

    }
}