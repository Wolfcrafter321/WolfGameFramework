using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;

using System.Reflection;

namespace Wolf
{
    public class WolfEventGraphView : GraphView
    {
        private WolfEventNodeSearchWindow searchWindow;

        public WolfEventGraphView(WolfEventGraphWindow window)
        {
            SetupZoom(0.1f, 10f);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var minimap = new MiniMap();
            minimap.SetPosition(new Rect(10, 30, 200, 200));
            minimap.anchored = true;
            Add(minimap);

            searchWindow = ScriptableObject.CreateInstance<WolfEventNodeSearchWindow>();
            nodeCreationRequest = context =>
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
            searchWindow.Init(window, this);
            
        }
    }
}