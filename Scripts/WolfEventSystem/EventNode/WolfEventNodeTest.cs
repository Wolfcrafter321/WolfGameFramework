using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolf;


#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;
using System.Reflection;
#endif

namespace Wolf
{
    [EventNode]
    public class WolfEventNodeTest : WolfEventNodeBase
    {
        public new static string searchTreePath = "Test";

        [SerializeField]
        public new List<WolfEventConnectableVariableBase> values = new List<WolfEventConnectableVariableBase>
        {
            new WolfEventConnectableVariable<string>("TEST"),
            new WolfEventConnectableVariable<int>(0)
        };

        public override IEnumerator ProcessEvent(WolfEventData source)
        {
            yield return base.ProcessEvent(source);
        }


    }
}