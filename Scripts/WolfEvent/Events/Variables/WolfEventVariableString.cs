using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [VariableNode]
    public class WolfEventVariableString : WolfEventBase, IWolfEventVariable
    {
        [NodeField]public string value;

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