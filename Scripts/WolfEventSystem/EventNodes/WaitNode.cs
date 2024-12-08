using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolf;

namespace WolfEventNode
{
    [EventNode]
    public class WaitNode : WolfEventNodeBase
    {
        public new static string searchTreePath = "Wait";

        public new List<WolfEventConnectableVariableBase> values = new List<WolfEventConnectableVariableBase>
        {
            new WolfEventConnectableVariable<float>("WaitTime", 1f)
        };

        public override IEnumerator ProcessEvent(WolfEventData source)
        {
            yield return new WaitForSeconds((float)values[0].GetValue(source));
            yield return base.ProcessEvent(source);
        }
    }
}
