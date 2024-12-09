using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.PackageManager.UI;
using System.Reflection;
using System;
using UnityEditor.SceneManagement;



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
        public WolfEventData targetData;

        /// <summary>
        /// ノードエディターを開きます
        /// </summary>
        /// <returns>WolfEventGraphWindow : Windowクラスが帰ってきます。</returns>
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

        void InitializeGraphView()
        {
            view = new WolfEventGraphView(this);
            view.StretchToParentSize();
            rootVisualElement.Add(view);
        }

        void CreateToolBar()
        {
            var tb = new Toolbar();

            var btn1 = new ToolbarButton(clickEvent: () => { SaveEvents(); }){ text = "Save", tooltip = "Save nodes to selected object."};
            var btn2 = new ToolbarButton(clickEvent: () => { LoadFromSelectedEvents(); }) { text = "Load", tooltip = "Load nodes from selected object." };
            var btn3 = new ToolbarButton(clickEvent: () => { view.ClearEventNodes(); }) { text = "Clear" };
            var btn4 = new ToolbarButton(clickEvent: () => { view.ToggleMiniMap(); }) { text = "MiniMap" };
            var btn5 = new ToolbarButton(clickEvent: () => { view.Test(); }) { text = "Test" };
            tb.Add(btn1);
            tb.Add(btn2);
            tb.Add(btn3);
            tb.Add(btn4);
            tb.Add(btn5);
            rootVisualElement.Add(tb);

        }

        void SaveEvents()
        {
            if (targetData != null)
            {
                //var result = EditorUtility.DisplayDialog(
                //    "Save Confirmation",  "Do you want to save your changes?", "OK", "Cancel");
                //if (!result) return;

                view.SaveEvents(targetData);
            }

            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }

        public void LoadEvent(WolfEventData data)
        {
            targetData = data;
            view.LoadEvents(data);
            titleContent = new GUIContent(text: data.ToString());
        }

        void LoadFromSelectedEvents()
        {
            // find from scene
            if(Selection.activeGameObject != null && Selection.count == 1)
            {
                WolfEventSystem target = null;
                Selection.activeTransform.gameObject.TryGetComponent<WolfEventSystem>(out target);

                if (view != null && target != null)
                {
                    view.LoadEvents(target.wolfEvent);
                    titleContent = new GUIContent(text: target.ToString());
                }
            } 
        }


    }
}
#endif