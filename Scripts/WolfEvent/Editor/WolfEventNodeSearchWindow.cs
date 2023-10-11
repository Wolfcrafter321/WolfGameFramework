using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;

namespace Wolf
{
    public class WolfEventNodeSearchWindow : ScriptableObject, ISearchWindowProvider
    {

        private WolfEventGraphView wegv;
        private EditorWindow wew;

        public void Init(EditorWindow window, WolfEventGraphView wegv)
        {
            this.wegv = wegv;
            this.wew = window;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>{ new SearchTreeGroupEntry(new GUIContent(text:"New EventNode"), level:0) };

            List<string> pathes = new List<string>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
                if (type.BaseType == typeof(WolfEventBase))
                {
                    var pathProperty = type.GetField("searchTreePath");
                    var path = (string)pathProperty.GetValue(type);

                    var splitted = path.Split("/");
                    for (int i = 0; i < splitted.Length; i++)
                    {
                        if (i == splitted.Length - 1)
                        {
                            var ste = new SearchTreeEntry(new GUIContent(text: splitted[i]));       // ToDo: 現状、同じ階層を正しく処理していない
                            ste.level = i + 1;
                            ste.userData = type;
                            tree.Add(ste);
                        }
                        else
                        {
                            var stge = new SearchTreeGroupEntry(new GUIContent(text: splitted[i]), level: i + 1);
                            tree.Add(stge);
                        }
                    }
                }

            return tree;
            // throw new System.NotImplementedException();
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            Debug.Log(searchTreeEntry.userData);
            var position = context.screenMousePosition - wew.position.position;

            Node n = new Node();
            
            n.title = searchTreeEntry.userData.ToString().Replace("Wolf.WolfEvent","");
            n.SetPosition(new Rect(position, Vector2.one));
            var type =  Type.GetType(searchTreeEntry.userData.ToString());
            var fields = type.GetFields();
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

                        if (field.FieldType.ToString() == "System.String")  n.mainContainer.Add(new TextField());
                        if (field.FieldType.ToString() == "System.Int")     n.mainContainer.Add(new IntegerField());
                        if (field.FieldType.ToString() == "System.Float")   n.mainContainer.Add(new FloatField());
                    }
                }
            }
            n.RefreshPorts();

            wegv.AddElement(n);

            return true;
        }
    }
}