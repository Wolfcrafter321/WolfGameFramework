using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    public class WolfEventSendMessage : WolfEventBase
    {
        public GameObject target;
        public WolfEventConnectableVariable<string> message;

        public override IEnumerator ProcessEvent(WolfEventSO source)
        {
            target.SendMessage(message.value);
            return base.ProcessEvent(source);
        }

    }
}