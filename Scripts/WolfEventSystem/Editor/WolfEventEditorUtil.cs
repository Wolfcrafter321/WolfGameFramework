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
            WolfEventGraphEditorNode n = new WolfEventGraphEditorNode();
            n.typeName = nodeType.ToString();

            string title = nodeType.ToString().Replace("Wolf.WolfEvent", "").Replace("Node", "").Replace("Variable", "");
            n.title = title;
            var type = Type.GetType(nodeType.ToString());
            var fields = type.GetFields();

            // クラスタイプでポート作成
            foreach (var attr in type.GetCustomAttributes())
            {
                // ノードのアトリビュートの種類に応じて、ノードのポートを付与していきます
                if (attr.ToString() == WolfEventNodeAttributeNames.FunctionNodeAttribute)
                {
                    var po = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Node));
                    po.portName = "Out";
                    po.name = "Out";
                    n.outputContainer.Add(po);
                    break;
                }
                if (attr.ToString() == WolfEventNodeAttributeNames.EventNodeAttribute)
                {
                    var pi = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Node));
                    var po = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Node));
                    pi.portName = "In";
                    po.portName = "Out";
                    pi.name = "In";
                    po.name = "Out";

                    n.inputContainer.Add(pi);
                    n.outputContainer.Add(po);
                    break;
                }
            }

            // フィールドの追加
            var tempinst = ScriptableObject.CreateInstance(nodeType) as WolfEventNodeBase;
            var valuesField = nodeType.GetField("values");
            foreach (var item in valuesField.GetValue(tempinst) as IEnumerable<object>)
            {
                Type itemT = item.GetType();
                var valField = itemT.GetField("value");
                var val = valField.GetValue(item);
                if (itemT.ToString().Contains("Wolf.WolfEventConnectableVariable"))
                {
                    string typeText = itemT.ToString().Split("[")[1].Split("]")[0];
                    Type fTyp = Type.GetType(typeText);
                    switch (typeText)
                    {
                        case "System.String":
                            var strF = new TextField();
                            if (val != null) strF.value = (string)val;
                            n.mainContainer.Add(strF);
                            break;
                        case "System.Int":
                            var intF = new IntegerField();
                            if (val != null) intF.value = (int)val;
                            n.mainContainer.Add(intF);
                            break;
                        case "System.Int32":
                            var int32F = new IntegerField();
                            if (val != null) int32F.value = (int)val;
                            n.mainContainer.Add(int32F);
                            break;
                        case "System.Float":
                            var floatF = new FloatField();
                            if (val != null) floatF.value = (float)val;
                            n.mainContainer.Add(floatF);
                            break;
                    }
                    var pi = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, fTyp);
                    var po = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, fTyp);
                    n.inputContainer.Add(pi);
                    n.outputContainer.Add(po);
                }
            }

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