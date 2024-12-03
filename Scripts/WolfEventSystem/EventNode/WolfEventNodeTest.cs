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
    [System.Serializable, EventNode]
    public class WolfEventNodeTest : WolfEventNodeBase
    {


        public new static string searchTreePath = "Test";

        public override IEnumerator ProcessEvent(WolfEventData source)
        {
            Debug.Log("whoooaaa");
            yield return base.ProcessEvent(source);
        }

#if UNITY_EDITOR
        public new static WolfEventNodeTest CreateNodeInstance(Node node)
        {
            var d = CreateInstance<WolfEventNodeTest>();
            d.position = node.GetPosition().position;
            return d;
        }
#endif
    }
}