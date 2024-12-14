using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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
        public static WolfEventGraphView instance;

        private WolfEventNodeSearchWindow searchWindow;

        private Label textLabel;
        private MiniMap map;
        protected Vector2 mousePos;

        public WolfEventGraphView(WolfEventGraphWindow window)
        {
            instance = this;
            name = "GraphView";

            SetupZoom(0.02f, 7f);

            styleSheets.Add(Resources.Load<StyleSheet>("Editor/GridBackground"));
            var gb = new GridBackground(); gb.name = "Grid Background"; 
            Insert(0, gb);
            gb.StretchToParentSize();

            var textlabelWrap = new VisualElement();
            textlabelWrap.pickingMode = PickingMode.Ignore;
            textlabelWrap.styleSheets.Add(Resources.Load<StyleSheet>("Editor/GraphViewLabelWrap"));
            textLabel = new Label();
            textLabel.pickingMode = PickingMode.Ignore;
            textLabel.name = "DebugInfoText";
            textLabel.text = "DebugInfoText - Hello!\nABC\nDEF";
            textLabel.style.marginTop = StyleKeyword.Auto;
            textlabelWrap.Add(textLabel);
            Add(textlabelWrap);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new KeyDownManipulator());

            map = new MiniMap();
            map.SetPosition(new Rect(10, 30, 100, 100));
            map.visible = false;
            Add(map);


            searchWindow = ScriptableObject.CreateInstance<WolfEventNodeSearchWindow>();
            nodeCreationRequest = context =>
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
            searchWindow.Init(window, this);


            RegisterCallback<MouseMoveEvent>(ctx => {
                mousePos = ctx.localMousePosition;
                string moji = "";
                moji += $"mouse local pos {ctx.localMousePosition.ToString()}\n";
                moji += $"mouse graph pos {contentViewContainer.WorldToLocal(ctx.localMousePosition).ToString()}\n";
                textLabel.text = moji;

            });


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

        
        public void LoadEvents(WolfEventData data)
        {
            ClearEventNodes();

            var connectInfos = new List<Node[]>();
            var nodes = new List<Node>();

            //create nodes
            foreach (var wolfEvent in data.wolfEvents)
            {
                var n = WolfEventEditorUtil.CreateUIElementNode(wolfEvent.GetType());
                n.SetData(wolfEvent);
                n.nodeName = wolfEvent.name;
                AddElement(n);
                nodes.Add(n);
            }
            // LookUp Connectivities
            for (var i = 0; i < data.wolfEvents.Count; i++)
            {
                var wolfEvent = data.wolfEvents[i];
                if (wolfEvent.targetEvent[0] != -1)
                    connectInfos.Add(new Node[2] { nodes[i], nodes[wolfEvent.targetEvent[0]] });
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
                // Node‚ÅID‚ðŽQÆ‚Å‚«‚é‚æ‚¤‚É‚µ‚Ü‚·
                allNodeIDDict.Add(allNodes[i] as WolfEventGraphEditorNode, i);
            }

            foreach (WolfEventGraphEditorNode node in nodes)
            {
                var typ = Type.GetType(node.typeName);
                //Debug.Log(typ); // WolfEventNodeBase , Teste, Wait ....
                var newData = ScriptableObject.CreateInstance(typ) as WolfEventNodeBase;
                newData.name = node.nodeName != null? node.nodeName : Guid.NewGuid().ToString();
                newData.position = node.GetPosition().position;
                newData.InitFields(newData);

                // save fields
                Dictionary<string, object> fieldData = new Dictionary<string, object>();
                foreach (WolfEventGraphEditorConnectableFieldWrapper item in node.fieldContainer.Children())
                {
                    fieldData.Add(item.fieldName, item.GetData());
                    Debug.Log(item.fieldName);
                }
                var cFields = typ.GetFields(BindingFlags.Instance | BindingFlags.Public)
                        .Where(type => type.ToString().Contains("Wolf.ConnectableVariable"));
                foreach (var f in cFields)
                {
                    var insf = ScriptableObject.CreateInstance(f.FieldType);    // ConnectableField
                    f.SetValue(newData, insf);
                    insf.GetType().GetField("value").SetValue(insf, fieldData[f.Name]);
                }

                // main Connection
                if (node.Q<Port>("Out") != null && node.Q<Port>("Out").connected)
                {
                    foreach (var e in node.Q<Port>("Out").connections)
                    {
                        var targNode = e.input.node as WolfEventGraphEditorNode;
                        newData.targetEvent = new int[1] { allNodeIDDict[targNode] };
                    }
                }
                //// fields connections
                //var fields = node.fieldContainer.Children().ToArray();
                //for (int j = 0; j < fields.Length; j++)
                //{
                //    var field = fields[j] as WolfEventGraphEditorConnectableFieldWrapper;
                //    newData.values[j].SetValue(field.GetData());
                //}


                targ.wolfEvents.Add(newData);
            }
        }

        public void Test()
        {
            var allNodes = nodes.ToList();
            var targ = UnityEngine.Random.Range(0, allNodes.Count -1);
            for (var i = 0; i < allNodes.Count; i++)
            {
                var n = (WolfEventGraphEditorNode)allNodes[i];
                n.orangeLine.visible = i == targ? true : false;
            }
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

        public void ToggleMiniMap()
        {
            map.visible = !map.visible;
        }

        public static Vector2 GetGraphMousePos()
        {
            return instance.contentViewContainer.WorldToLocal(instance.mousePos);
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

            //if (evt.ctrlKey && !evt.shiftKey && evt.keyCode == KeyCode.Z)
            //    Undo.PerformUndo();
            //if (evt.ctrlKey && evt.shiftKey && evt.keyCode == KeyCode.Z)
            //    Undo.PerformRedo();

            if (!evt.ctrlKey)
            {
                //if (evt.keyCode == KeyCode.C)
                //    if (target is WolfEventGraphView view)
                //        view.CreateComment();
                //if (evt.keyCode == KeyCode.G)
                //    if (target is WolfEventGraphView view)
                //        view.CreateGroup();
            }
        }
    }
}
#endif