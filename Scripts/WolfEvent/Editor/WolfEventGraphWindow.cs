using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;

namespace Wolf
{
    public class WolfEventGraphWindow : EditorWindow
    {
        private WolfEventGraphView view;
        private WolfEventManager target;

        [MenuItem("Wolf/W/o/l/f/WolfEventGraphWindow")]
        public static void OpenWolfEventGraphWindow()
        {
            var window = GetWindow<WolfEventGraphWindow>();
            window.titleContent = new GUIContent(text: "Event Graph Editor :)");
        }

        private void OnEnable()
        {
            InitializeGraphView();
            CreateToolBar();
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(view);
        }

        void InitializeGraphView()
        {
            view = new WolfEventGraphView(this){name = "GraphView" };
            view.StretchToParentSize();
            rootVisualElement.Add(view);
        }

        void CreateToolBar()
        {
            var tb = new Toolbar();

            var btn1 = new ToolbarButton(clickEvent: () => { Debug.Log("hi!"); });
            btn1.text = "hi!";
            tb.Add(btn1);
            rootVisualElement.Add(tb);

        }


    }
}