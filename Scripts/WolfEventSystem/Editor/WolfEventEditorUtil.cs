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

        public static Node CreateUIElementNode(WolfEventNodeBase wolfEvent)
        {

            Type eventType = wolfEvent.GetType();
            Node n = CreateUIElementNode(eventType);
            n.SetPosition(new Rect(wolfEvent.position.x, wolfEvent.position.y, 0, 0));
            return n;
        }

        public static Node CreateUIElementNode(Type eventType)
        {
            WolfEventGraphEditorNode n = new WolfEventGraphEditorNode();
            n.typeName = eventType.ToString();

            string title = eventType.ToString().Replace("Wolf.WolfEventVariable", "").Replace("Wolf.WolfEvent", "");
            n.title = title;
            var type = Type.GetType(eventType.ToString());
            var fields = type.GetFields();


            foreach (var attr in type.GetCustomAttributes())        // イベント接続のポート
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
            foreach (var field in fields)        // 変数接続のポート
            {
                foreach (var attr in field.GetCustomAttributes())
                {
                    if (attr.ToString() == WolfEventNodeAttributeNames.NodeConnectableFieldAttribute)
                    {
                        Debug.Log(attr);
                        Debug.Log(field.FieldType.ToString());
                        if (field.FieldType.ToString() == WolfEventNodeAttributeNames.NodeConnectableFieldAttribute)
                        {
                            n.mainContainer.Add(new TextField());
                        }
                        var pi = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(string));
                        var po = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(string));
                        n.inputContainer.Add(pi);
                        n.outputContainer.Add(po);
                    }
                }
            }
            foreach (var field in fields)        // フィールド
            {
                foreach (var attr in field.GetCustomAttributes())
                {
                    if (attr.ToString() == WolfEventNodeAttributeNames.NodeFieldAttribute)
                    {
                        Debug.Log(field.FieldType);

                        if (field.FieldType.ToString() == "System.String") n.mainContainer.Add(new TextField());
                        if (field.FieldType.ToString() == "System.Int") n.mainContainer.Add(new IntegerField());
                        if (field.FieldType.ToString() == "System.Float") n.mainContainer.Add(new FloatField());
                    }
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