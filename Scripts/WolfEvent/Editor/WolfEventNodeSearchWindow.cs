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
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent(text:"New EventNode"), level:0),
                new SearchTreeGroupEntry(new GUIContent(text:"Test"), level:1),
                new SearchTreeEntry(new GUIContent(text:"Testo!")){userData = typeof(EventNode), level = 2}     // ToDo:é©ìÆÇ≈åüçıÇ≈Ç´Ç»Ç¢Ç©ÅHÅB
            };
            return tree;
            // throw new System.NotImplementedException();
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var position = context.screenMousePosition - wew.position.position;

            switch (searchTreeEntry.userData)
            {
                case Wolf.EventNode:
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