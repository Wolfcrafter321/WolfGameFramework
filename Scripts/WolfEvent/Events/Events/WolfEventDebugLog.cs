using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    public class WolfEventDebugLog : WolfEventBase
    {

        public string text;

        public override IEnumerator ProcessEvent(WolfEventSO source)
        {
            Debug.Log(text);
            return base.ProcessEvent(source);
        }
    }
}