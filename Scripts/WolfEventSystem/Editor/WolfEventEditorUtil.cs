using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Codice.CM.WorkspaceServer.DataStore;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;


namespace Wolf
{
    public static class WolfEventEditorUtil
    {
        public static WolfEventNodeBase CreateNodeInstance(Type nodeType)
        {
            var inst = ScriptableObject.CreateInstance(nodeType) as WolfEventNodeBase;
            inst.InitFields(inst);
            return inst;
        }

        public static WolfEventGraphEditorNode CreateUIElementNode(Type nodeType)
        {
            WolfEventGraphEditorNode n = new WolfEventGraphEditorNode(nodeType);
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