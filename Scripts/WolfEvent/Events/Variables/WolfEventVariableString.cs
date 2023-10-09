using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    public class WolfEventVariableString : WolfEventBase, IWolfEventVariable
    {
        public string value;

        public static new string searchTreePath = "Variables/String";

        public override IEnumerator ProcessEvent(WolfEventSO source)
        {
            return base.ProcessEvent(source);
        }

        public object GetValue()
        {
            return value;
        }

    }
}