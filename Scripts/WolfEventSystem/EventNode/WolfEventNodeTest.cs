using System.Collections;
using UnityEngine;
using Wolf;

namespace Wolf
{
    [WolfEventNode, NodeSearchPath("Event/Test")]
    public class WolfEventNodeTest : WolfEventNodeBase
    {

        public ConnectableVariableString test;

        public override IEnumerator ProcessEvent(WolfEventData source)
        {
            Debug.Log($"This is TEST NODE! {test.GetValue(source)}");
            yield return base.ProcessEvent(source);
        }


    }
}