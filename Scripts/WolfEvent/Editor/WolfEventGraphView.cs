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
            SetupZoom(0.02f, 5f);

            styleSheets.Add(Resources.Load<StyleSheet>("GridBackground"));
            GridBackground gb = new GridBackground();
            Insert(0, gb);
            gb.StretchToParentSize();

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            /*
            var minimap = new MiniMap();
            minimap.SetPosition(new Rect(10, 30, 100, 100));
            Add(minimap);
            */

            searchWindow = ScriptableObject.CreateInstance<WolfEventNodeSearchWindow>();
            nodeCreationRequest = context =>
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
            searchWindow.Init(window, this);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new  List<Port>();
            ports.ForEach((port) => {
                switch (port.direction)
                {
                    case Direction.Input:
                        break;
                    case Direction.Output:
                        break;
                }
                if (startPort != port && 
                    startPort.node != port.node && 
                    startPort.direction != port.direction && 
                    startPort.portType == port.portType
                    ) 
                    compatiblePorts.Add(port);
            });
            return compatiblePorts;
            //return base.GetCompatiblePorts(startPort, nodeAdapter);
        }

        /// <summary>
        /// 選択物からイベントノードを取得します。
        /// SOの場合は単品、コンポーネントの場合は複数のSOを取得。
        /// </summary>
        /// <param name="wManager"></param>
        public void LoadEvents(WolfEventManager wManager)
        {
            Debug.Log(wManager);
            Debug.Log("view Load!");

        }

        public void SaveEvents(WolfEventManager wManager)
        {
            Debug.Log(wManager);
            Debug.Log("view Save!");
        }

        public void TestEvents()
        {
            ports.ForEach((port) => { Debug.Log(port); });
            
        }
    }
}