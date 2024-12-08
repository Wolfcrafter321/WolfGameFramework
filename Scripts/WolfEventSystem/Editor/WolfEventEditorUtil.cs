using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;


namespace Wolf
{
    public class WolfEventEditorUtil
    {

        public static WolfEventGraphEditorNode CreateUIElementNode(WolfEventNodeBase node)
        {
            Type nodeType = node.GetType();
            WolfEventGraphEditorNode n = CreateUIElementNode(nodeType);
            n.SetPosition(new Rect(node.position.x, node.position.y, 0, 0));
            return n;
        }

        public static WolfEventGraphEditorNode CreateUIElementNode(Type nodeType)
        {
            WolfEventGraphEditorNode n = new WolfEventGraphEditorNode(nodeType);
            n.RefreshPorts();
            return n;
        }

        public static Edge ConnectTwoNodes(Node a, Node b)
        {
            var out_a = a.Q<Port>("Out");
            var in_b = b.Q<Port>("In");
            return ConnectTwoPorts(out_a, in_b);
        }

        public static Edge ConnectTwoPorts(Port a, Port b)
        {
            if(a == null || b == null) return null;
            return a.ConnectTo(b); 
        }
    }
}
#endif