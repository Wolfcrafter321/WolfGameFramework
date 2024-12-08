using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        public List<WolfEventGraphEditorConnectableFieldBase> fields;

        public VisualElement orangeLine;

        public WolfEventGraphEditorNode()
        {
            styleSheets.Remove(Resources.Load<StyleSheet>("Editor/GridBackground"));

            // create current outline
            orangeLine = new VisualElement();
            orangeLine.name = "CurrentHightlight";
            orangeLine.styleSheets.Add(Resources.Load<StyleSheet>("Editor/NodeCurrent"));
            orangeLine.pickingMode = PickingMode.Ignore;
        }

        public void AddConnectableField<T>()
        {
            if (fields == null) fields = new List<WolfEventGraphEditorConnectableFieldBase>();

            Type t = typeof(T);
            switch (t.ToString())
            {
                default:
                    break;
            }
        }

        public void SetData(WolfEventNodeBase n)
        {
            Debug.Log("Set Node Data : " + n.ToString());

            var valuesField = n.GetType().GetField("values");
            foreach (var item in valuesField.GetValue(n) as IEnumerable<object>)
            {

            }
        }

    }
    public abstract class WolfEventGraphEditorConnectableFieldBase : VisualElement
    {
    }

    public class WolfEventGraphEditorConnectableField<T> : WolfEventGraphEditorConnectableFieldBase
    {
        public WolfEventGraphEditorConnectableField()
        {

        }
    }
}
#endif