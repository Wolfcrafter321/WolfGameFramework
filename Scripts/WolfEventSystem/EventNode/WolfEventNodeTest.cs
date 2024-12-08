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
            new WolfEventConnectableVariable<string>("LogMoji", "TEST")
            //new WolfEventConnectableVariable<int>(0),
            //new WolfEventConnectableVariable<GameObject>(null),
            //new WolfEventConnectableVariable<GameObject>(null)

        };

        public override IEnumerator ProcessEvent(WolfEventData source)
        {
            Debug.Log($"This is TEST NODE! {values[0].GetValue(source)}");
            yield return base.ProcessEvent(source);
        }


    }
}