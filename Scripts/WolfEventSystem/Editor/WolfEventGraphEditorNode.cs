using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor.UIElements;
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
        public Dictionary<string, WolfEventGraphEditorConnectableFieldWrapper> fields;

        public WolfEventGraphEditorNode(Type nodeType)
        {
            this.fields = new Dictionary<string, WolfEventGraphEditorConnectableFieldWrapper>();
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
                if (attr.ToString() == "Wolf.WolfEventNodeAttribute")
                {
                    var pi = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Node));
                    var po = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Node));
                    pi.portName = "In";
                    po.portName = "Out";
                    pi.name = "In";
                    po.name = "Out";

                    inputContainer.Add(pi);
                    outputContainer.Add(po);
                }
                else if (attr.ToString() == "Wolf.WolfEventFunctionNodeAttribute")
                {
                    var po = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Node));
                    po.portName = "Out";
                    po.name = "Out";
                    outputContainer.Add(po);
                }
                else if (attr.ToString() == "Wolf.WolfEventVariableNodeAttribute")
                {
                }
            }

            // 新規でフィールドの追加
            var tempinst = ScriptableObject.CreateInstance(nodeType) as WolfEventNodeBase;
            var cFields = nodeType.GetFields(BindingFlags.Instance | BindingFlags.Public)
                    .Where(type => type.ToString().Contains("Wolf.Connectable"));
            foreach (var f in cFields)
            {
                if (!f.FieldType.ToString().Contains("Wolf.ConnectableVariable")) continue;
                var fTyp = f.FieldType.GetField("value");
                AddConnectableField(fTyp.FieldType, label:f.Name, f.GetValue(tempinst));
            }
        }

        public void AddConnectableField(Type t, string label, object defaultValue = null)
        {
            var wrapper = new WolfEventGraphEditorConnectableFieldWrapper(t, label);

            switch (t.ToString())
            {
                case "System.String":
                    var strF = new TextField();
                    strF.label = label;
                    strF.multiline = true;
                    strF.value = defaultValue != null ? (string)defaultValue : "";
                    wrapper.fieldContainer.Add(strF);
                    wrapper.onGetValue += () => { return strF.value; };
                    wrapper.onSetValue += (data) => { strF.value = (string)data; };
                    break;
                case "System.Int":
                case "System.Int32":
                    var intF = new IntegerField() { label = name };
                    intF.label = label;
                    intF.value = defaultValue != null ? (int)defaultValue : 0;
                    wrapper.fieldContainer.Add(intF);
                    wrapper.onGetValue += () => { return intF.value; };
                    wrapper.onSetValue += (data) => { intF.value = (int)data; };
                    break;
                case "System.Single":
                    var floatSF = new FloatField() { label = name };
                    floatSF.label = label;
                    floatSF.value = defaultValue != null ? (float)defaultValue : 0f;
                    wrapper.fieldContainer.Add(floatSF);
                    wrapper.onGetValue += () => { return floatSF.value; };
                    wrapper.onSetValue += (data) => { floatSF.value = (float)data; };
                    break;
                case "System.Float":
                    var floatF = new FloatField() { label = name };
                    floatF.label = label;
                    floatF.value = defaultValue != null ? (float)defaultValue : 0f;
                    wrapper.fieldContainer.Add(floatF);
                    wrapper.onGetValue += () => { return floatF.value; };
                    wrapper.onSetValue += (data) => { floatF.value = (float)data; };
                    break;
                case "UnityEngine.AudioClip":
                    var goF = new ObjectField() { label = name };
                    goF.objectType = t;
                    goF.label = label;
                    goF.value = defaultValue != null ? (UnityEngine.Object)defaultValue : null;
                    wrapper.fieldContainer.Add(goF);
                    wrapper.onGetValue += () => { return goF.value; };
                    wrapper.onSetValue += (data) => { goF.value = (UnityEngine.Object)data; };
                    break;
            }

            var pi = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, t);
            var po = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, t);
            wrapper.inputContainer.Add(pi);
            wrapper.outputContainer.Add(po);
            wrapper.fieldContainer.Add(wrapper);

            fieldContainer.Add(wrapper);
            fields.Add(label, wrapper);
        }

        public void SetData(WolfEventNodeBase n)
        {
            SetPosition(new Rect(n.position, Vector2.one));
            var valuesField = n.GetType().GetField("values");

            var cFields = n.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                        .Where(type => type.ToString().Contains("Wolf.ConnectableVariable"));
            foreach (var cField in cFields)
            {
                var cfValField = cField.GetValue(n).GetType().GetField("value").GetValue(cField.GetValue(n));
                //Debug.Log($"{cField.Name} -- {cfValField}");
                fields[cField.Name].SetData(cfValField);
            }

            n.onStartEvent += () =>{ SetNodeActiveState(true); };
            n.onEndEvent += () =>{ SetNodeActiveState(false); };
        }

        public void SetNodeActiveState(bool activ)
        {
            orangeLine.visible = activ;
        }

        void OnDiasble()
        {
            Debug.Log("Goodbye!");
        }
    }

    public class WolfEventGraphEditorConnectableFieldWrapper : VisualElement
    {
        public Type fieldType;
        public string fieldName;

        public VisualElement fieldContainer;
        public VisualElement inputContainer;
        public VisualElement outputContainer;

        public WolfEventGraphEditorConnectableFieldWrapper(Type fieldType, string label)
        {
            this.fieldName = label;
            this.fieldType = fieldType;
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

        public delegate object OnGetValue();
        public OnGetValue onGetValue;
        public delegate void OnSetValue(object data);
        public OnSetValue onSetValue;

        public object GetData()
        {
            if(fieldType == null) return -1;
            if(fieldContainer == null) return -1;

            if(onGetValue != null) return onGetValue();

            return null;
        }

        public void SetData(object data)
        {
            onSetValue?.Invoke(data);
        }
    }
}
#endif