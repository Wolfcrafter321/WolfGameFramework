using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;
using WolfEventNode;

namespace Wolf
{
    public class WolfEventSystem : MonoBehaviour
    {

        public WolfEventData wolfEvent;

        private List<int> startEvent;
        private List<int> updateEvent;
        private Dictionary<string, int> customEvent;


        private void Start()
        {
            if (startEvent != null && startEvent.Count > 0)
                foreach (var node in startEvent)
                {
                    wolfEvent.StartEventAt(node, this);
                }
        }

        private void Update()
        {
            if(updateEvent != null && updateEvent.Count > 0)
                foreach (var node in updateEvent)
                {
                    wolfEvent.StartEventAt(node, this);
                }
        }

        public void InvokeEvent(string eventName)
        {
            if (customEvent.ContainsKey(eventName))
            {
                wolfEvent.StartEventAt(this, customEvent[eventName]);
            }
        }

        [ContextMenu("Test Start from 0")]
        public void InvokeFrom0()
        {
            wolfEvent.StartEventAt(this, 0);
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(WolfEventSystem))]
    public class WolfEventSystemEditor : Editor
    {

        private WolfEventSystem targ;

        public override void OnInspectorGUI()
        {
            targ = this.target as WolfEventSystem;

            base.OnInspectorGUI();

            if (GUILayout.Button("CreateEventSO"))
            {
                var data = ScriptableObject.CreateInstance<WolfEventData>();
                data.name = targ.name + "'s Event";
                var node1 = WolfEventEditorUtil.CreateNodeInstance(typeof(WolfEventNodeTest)) as WolfEventNodeTest;
                var node2 = WolfEventEditorUtil.CreateNodeInstance(typeof(WolfEventNodeTest)) as WolfEventNodeTest;
                var node3 = WolfEventEditorUtil.CreateNodeInstance(typeof(WolfEventNodeTest)) as WolfEventNodeTest;
                var node4 = WolfEventEditorUtil.CreateNodeInstance(typeof(WolfEventNodeTest)) as WolfEventNodeTest;
                node1.position = new Vector2(000, 000);
                node2.position = new Vector2(300, 050);
                node3.position = new Vector2(600, 100);
                node4.position = new Vector2(900, 150);
                node1.targetEvent[0] = 1;
                node2.targetEvent[0] = 2;
                node3.targetEvent[0] = 3;
                node2.test.inputSideConnection = new ConnectionInfo() { targetNode = 0, targetSlot = 0 };
                node4.test.inputSideConnection = new ConnectionInfo() { targetNode = 2, targetSlot = 0 };
                node1.test.value = "A";
                node2.test.value = "B";
                node3.test.value = "C";
                node4.test.value = "D";
                data.wolfEvents.Add(node1);
                data.wolfEvents.Add(node2);
                data.wolfEvents.Add(node3);
                data.wolfEvents.Add(node4);
                targ.wolfEvent = data;

                AssetDatabase.SaveAssets();
                EditorUtility.SetDirty(targ);
            }

            if (GUILayout.Button("Open Editor Window", GUILayout.Height(29)))
            {
                var w = WolfEventGraphWindow.OpenWolfEventGraphWindow();
                w.LoadEvent(targ.wolfEvent);
            }

            if (GUILayout.Button("Debug", GUILayout.Height(29)))
            {
                foreach (var item in targ.wolfEvent.wolfEvents)
                {
                    Debug.Log($"event node - {item} - {item.GetValue(0)}");
                }
            }

            if (GUILayout.Button("Debug InvokeFrom0", GUILayout.Height(29)))
            {
                targ.InvokeFrom0();
            }

            serializedObject.ApplyModifiedProperties();
        }

    }
#endif

}