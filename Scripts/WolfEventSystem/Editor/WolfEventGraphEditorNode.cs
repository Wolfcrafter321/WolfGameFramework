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
        public VisualElement fieldContainer;

        public VisualElement orangeLine;

        public WolfEventGraphEditorNode(Type nodeType)
        {
            typeName = nodeType.ToString();
            title = nodeType.ToString();
            var fields = nodeType.GetFields();

            styleSheets.Remove(Resources.Load<StyleSheet>("Editor/GridBackground"));

            // create current outline
            orangeLine = new VisualElement();
            orangeLine.name = "CurrentHightlight";
            orangeLine.styleSheets.Add(Resources.Load<StyleSheet>("Editor/NodeCurrent"));
            orangeLine.pickingMode = PickingMode.Ignore;
            orangeLine.visible = false;
            Insert(0, orangeLine);

            var div = new VisualElement() { name = "divider" };
            div.AddToClassList("horizontal");
            mainContainer.Add(div);

            // create fieldsContainer
            fieldContainer = new VisualElement() { name = "FieldContainer" };
            fieldContainer.style.color = new Color(0.2470588f, 0.2470588f, 0.2470588f, 0.8039126f);
            mainContainer.Add(fieldContainer);

            // create Ports
            // アトリビュートの種類でポート作成
            foreach (var attr in nodeType.GetCustomAttributes())
            {
                if (attr.ToString() == WolfEventNodeAttributeNames.EventNodeAttribute)
                {
                    var pi = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Node));
                    var po = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Node));
                    pi.portName = "In";
                    po.portName = "Out";
                    pi.name = "In";
                    po.name = "Out";

                    inputContainer.Add(pi);
                    outputContainer.Add(po);
                    break;
                }
            }

            // フィールドの追加
            var tempinst = ScriptableObject.CreateInstance(nodeType) as WolfEventNodeBase;
            var valuesField = nodeType.GetField("values");
            foreach (var item in valuesField.GetValue(tempinst) as IEnumerable<object>)
            {
                Type itemT = item.GetType();
                var val = itemT.GetField("value").GetValue(item);
                string name = itemT.GetField("name").GetValue(item) as string;
                if (!itemT.ToString().Contains("Wolf.WolfEventConnectableVariable")) continue;

                string typeText = itemT.ToString().Split("[")[1].Split("]")[0];
                Type fTyp = Type.GetType(typeText);
                AddConnectableField(fTyp, val);
            }
        }


        public void AddConnectableField(Type t, object defaultValue = null)
        {
            var wrapper = new WolfEventGraphEditorConnectableFieldWrapper();

            switch (t.ToString())
            {
                case "System.String":
                    var strF = new TextField();
                    strF.label = "String";
                    strF.multiline = true;
                    strF.value = defaultValue != null ? (string)defaultValue : "";
                    wrapper.fieldContainer.Add(strF);
                    break;
                case "System.Int":
                case "System.Int32":
                    var intF = new IntegerField() { label = name };
                    intF.label = "Int";
                    intF.value = defaultValue != null ? (int)defaultValue : 0;
                    wrapper.fieldContainer.Add(intF);
                    break;
                case "System.Float":
                case "System.Single":
                    var floatF = new FloatField() { label = name };
                    floatF.label = "Float";
                    floatF.value = defaultValue != null ? (float)defaultValue : 0f;
                    wrapper.fieldContainer.Add(floatF);
                    break;
            }

            var pi = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, t);
            var po = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, t);
            wrapper.inputContainer.Add(pi);
            wrapper.outputContainer.Add(po);
            wrapper.fieldContainer.Add(wrapper);

            fieldContainer.Add(wrapper);
        }

        public void SetData(WolfEventNodeBase n)
        {
            var valuesField = n.GetType().GetField("values");
            
        }

    }
    public class WolfEventGraphEditorConnectableFieldWrapper : VisualElement
    {
        public VisualElement fieldContainer;
        public VisualElement inputContainer;
        public VisualElement outputContainer;

        public WolfEventGraphEditorConnectableFieldWrapper()
        {
            name = "FieldWrapper";
            styleSheets.Add(Resources.Load<StyleSheet>("Editor/NodeConnectableField"));

            inputContainer = new VisualElement() { name = "inputContainer" };
            outputContainer = new VisualElement() { name = "outputContainer" };
            fieldContainer = new VisualElement() { name= "fieldContainer" };
            var div1 = new VisualElement() { name = "divider" };
            div1.AddToClassList("vertical");
            var div2 = new VisualElement() { name = "divider" };
            div2.AddToClassList("vertical");

            Add(inputContainer);
            Add(div1);
            Add(fieldContainer);
            Add(div2);
            Add(outputContainer);
        }
    }
}
#endif