using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;

namespace Wolf
{
    public class WolfEventGraphWindow : EditorWindow
    {
        public WolfEventGraphView view;

        [MenuItem("Wolf/WolfEventGraphWindow")]
        public static WolfEventGraphWindow OpenWolfEventGraphWindow()
        {
            var window = GetWindow<WolfEventGraphWindow>();
            window.titleContent = new GUIContent(text: "Event Graph Editor :)");
            return window;
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

            var btn1 = new ToolbarButton(clickEvent: () => { SaveEvents(); }){ text = "Save", tooltip = "Save nodes to selected object."};
            var btn2 = new ToolbarButton(clickEvent: () => { LoadEvents(); }) { text = "Load", tooltip = "Load nodes from selected object." };
            var btn3 = new ToolbarButton(clickEvent: () => { view.ClearEventNodes(); }) { text = "Clear" };
            var btn4 = new ToolbarButton(clickEvent: () => { view.TestEvents(); }) { text = "TEST" };
            tb.Add(btn1);
            tb.Add(btn2);
            tb.Add(btn3);
            tb.Add(btn4);
            rootVisualElement.Add(tb);

        }

        void SaveEvents()
        {
            var target = GetWolfEventManagerFromSceneSelectedObject();
            Debug.Log("Save!"); if (view != null && target != null) view.SaveEvents(target);
        }
        
        void LoadEvents()
        {
            var target = GetWolfEventManagerFromSceneSelectedObject();
            Debug.Log("Load!"); if (view != null && target != null) view.LoadEvents(target);
        }

        WolfEventSO GetWolfEventManagerFromSceneSelectedObject()        // ToDO: スクリプタブルオブジェクトに対応する
        {
            WolfEventManager target = null;
            if(Selection.count == 1) Selection.activeTransform.gameObject.TryGetComponent<WolfEventManager>(out target);
            return target.wolfEventSOs[0];
        }


    }
}
#endif