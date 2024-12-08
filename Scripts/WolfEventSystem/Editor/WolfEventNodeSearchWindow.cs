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
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())    // getting all type ( inherit of WolfEventBase )
                if (type.BaseType == typeof(WolfEventNodeBase))
                {
                    var pathProperty = type.GetField("searchTreePath");
                    var path = (string)pathProperty.GetValue(type);             // like "debug/log"

                    var splitted = path.Split("/");
                    for (int i = 0; i < splitted.Length; i++)
                    {
                        if (i == splitted.Length - 1)
                        {
                            var ste = new SearchTreeEntry(new GUIContent(text: splitted[i]));       // ToDo: Œ»óA“¯‚¶ŠK‘w‚ð³‚µ‚­ˆ—‚µ‚Ä‚¢‚È‚¢
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
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var position = WolfEventGraphView.GetGraphMousePos();

            var type =  Type.GetType(searchTreeEntry.userData.ToString());
            WolfEventGraphEditorNode n = WolfEventEditorUtil.CreateUIElementNode(type);
            n.SetPosition(new Rect(position, Vector2.zero));

            wegv.Add(n);
            return true;
        }
    }
}
#endif