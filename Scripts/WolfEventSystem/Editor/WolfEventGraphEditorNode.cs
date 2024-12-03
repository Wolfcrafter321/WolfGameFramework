using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;
using System.Reflection;


namespace Wolf {
    public class WolfEventGraphEditorNode : Node
    {
        public string typeName;
        public string nodeName;

    }
}
#endif