using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#if UNITY_EDITOR
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
            SetupZoom(0.02f, 7f);

            styleSheets.Add(Resources.Load<StyleSheet>("Editor/GridBackground"));
            GridBackground gb = new GridBackground();
            Insert(0, gb);
            gb.StretchToParentSize();

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new KeyDownManipulator());

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

        public void ClearEventNodes()
        {
            foreach (var n in nodes)
            {
                RemoveElement(n);
            }
            foreach (var n in edges)
            {
                RemoveElement(n);
            }
            foreach(var c in graphElements)
            {

            RemoveElement(c); 
            }
        }

        /// <summary>
        /// 選択物からイベントノードを取得します。
        /// </summary>
        /// <param name="wManager"></param>
        public void LoadEvents(WolfEventData data)
        {
            //clear
            ClearEventNodes();

            var connectInfos = new List<Node[]>();
            var nodes = new List<Node>();

            //create nodes
            foreach (var wolfEvent in data.wolfEvents)
            {
                var n = WolfEventEditorUtil.CreateUIElementNode(wolfEvent);
                AddElement(n);
                nodes.Add(n);
            }
            for (var i = 0; i < data.wolfEvents.Count; i++)
            {
                var wolfEvent = data.wolfEvents[i];
                if (wolfEvent.targetEvent != -1)
                    connectInfos.Add(new Node[2] { nodes[i], nodes[wolfEvent.targetEvent] });
            }

            //connect nodes
            foreach(var connectInfo in connectInfos)
            {
                var e = WolfEventEditorUtil.ConnectTwoNodes(connectInfo[0], connectInfo[1]);
                if (e != null) AddElement(e);

            }

        }

        public void SaveEvents(WolfEventData targ)
        {
            targ.wolfEvents = new List<WolfEventNodeBase>();
            var allNodes = nodes.ToList();
            var allNodeIDDict = new Dictionary<WolfEventGraphEditorNode, int>();
            for (var i = 0; i < allNodes.Count; i++)
            {
                allNodeIDDict.Add(allNodes[i] as WolfEventGraphEditorNode, i);
            }
            for (var i = 0; i < allNodes.Count; i++)
            {
                var node = allNodes[i] as WolfEventGraphEditorNode;
                var typ = Type.GetType(node.typeName);
                if (typ == null) continue;

                MethodInfo method = typ.GetMethod("CreateNodeInstance", BindingFlags.Static | BindingFlags.Public);
                if (method == null) continue;
                var newData = method.Invoke(null, new object[1] { node }) as WolfEventNodeBase;
                if (node.Q<Port>("Out") != null && node.Q<Port>("Out").connected)
                {
                    foreach (var e in node.Q<Port>("Out").connections)
                    {
                        var targNode = e.input.node as WolfEventGraphEditorNode;
                        newData.targetEvent = allNodeIDDict[targNode];
                    }
                }
                targ.wolfEvents.Add(newData);
            }
        }

        public void Test()
        {
            var n = nodes.ToList()[0];
            n.style.borderTopWidth = 6;
            n.style.borderBottomWidth = 6;
            n.style.borderRightWidth = 6;
            n.style.borderLeftWidth = 6;
            var col = new StyleColor(new Color(0.8f, 0.4f, 0.4f));
            n.style.borderTopColor = col;
            n.style.borderBottomColor = col;
            n.style.borderRightColor = col;
            n.style.borderLeftColor = col;
        }

        public void CreateComment()
        {
            var sticky = new StickyNote
            {
                title = "NewComment"
            };
        }
        public void CreateGroup()
        {
            var g = new Group
            {
                title = "NewComment"
            };
            AddElement(g);
            foreach (var item in selection)
            {
                if(item is GraphElement el)
                {
                    g.AddElement(el);
                }
            }
        }
    }

    class KeyDownManipulator : Manipulator
    {
        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<KeyDownEvent>(OnKeyDown);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<KeyDownEvent>(OnKeyDown);
        }

        private void OnKeyDown(KeyDownEvent evt)
        {

            if (evt.ctrlKey && !evt.shiftKey && evt.keyCode == KeyCode.Z)
            {
                Undo.PerformUndo();
            }
            if (evt.ctrlKey && evt.shiftKey && evt.keyCode == KeyCode.Z)
            {
                Undo.PerformRedo();
            }

            if (!evt.ctrlKey)
            {
                if (evt.keyCode == KeyCode.C)
                {
                    if (target is WolfEventGraphView view)
                    {
                        view.CreateComment();
                    }
                }
                if (evt.keyCode == KeyCode.G)
                {
                    if (target is WolfEventGraphView view)
                    {
                        view.CreateGroup();
                    }
                }
            }
        }
    }
}
#endif