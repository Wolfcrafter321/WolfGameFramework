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
            var position = context.screenMousePosition - wew.position.position;

            Debug.Log(searchTreeEntry.userData);
            //var t = searchTreeEntry.userData.GetType();
            //var m = t.GetMethod("GetNodeCreationInfo");               // ここを、ノードさくせい（）を作って、タイプ内部のフィールドを回せないか？
            //Debug.Log(m.Invoke(searchTreeEntry.userData, null));
            

            switch (searchTreeEntry.userData)
            {
                case Wolf.WolfEventBase a:
                    Debug.Log(searchTreeEntry.userData + " " + context + "afddfsasdfsfdfs ");
                    break;
                default:
                    Debug.Log(searchTreeEntry.userData + " " + context + " ");
                    break;
            }
            return true;
            // throw new System.NotImplementedException();
        }
    }
}