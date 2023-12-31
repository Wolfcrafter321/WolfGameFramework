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

        public static void SaveWolfEvent(WolfEventSO so)
        {

        }


        public static Node CreateUIElementNode(WolfEventBase wolfEvent)
        {

            Type eventType = wolfEvent.GetType();
            Node n = CreateUIElementNode(eventType);
            n.SetPosition(new Rect(wolfEvent.nodeX, wolfEvent.nodeY, 0, 0));
            return n;
        }

        public static Node CreateUIElementNode(Type eventType)
        {
            Node n = new Node();

            string title = eventType.ToString().Replace("Wolf.WolfEventVariable", "").Replace("Wolf.WolfEvent", "");
            n.title = title;
            var type = Type.GetType(eventType.ToString());
            var fields = type.GetFields();

            Debug.LogFormat("test {0} {1} {2} {3}", eventType.ToString(), type, fields, "");

            foreach (var attr in type.GetCustomAttributes())        // イベント接続のポート
            {
                Debug.Log(attr);
                if (attr.ToString() == WolfEventNodeAttributeNames.FunctionNodeAttribute)
                {
                    var po = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Node));
                    po.portName = "Out";
                    n.outputContainer.Add(po);
                    break;
                }
                if (attr.ToString() == WolfEventNodeAttributeNames.NodeAttribute)
                {
                    var pi = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Node));
                    var po = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Node));
                    pi.portName = "In";
                    po.portName = "Out";

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
                //Debug.Log(member.Name + ": " + member.MemberType);
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

    }
}
#endif