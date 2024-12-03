using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;
using CodiceApp.EventTracking.Plastic;

namespace Wolf
{
    public class WolfEventSystem : MonoBehaviour
    {

        public WolfEventData wolfEvent;

        private List<int> startEvent;
        private List<int> updateEvent;
        private Dictionary<string, int> customEvent;

        private void Awake()
        {
            startEvent  = new List<int>();
            updateEvent = new List<int>();
            customEvent = new Dictionary<string, int>();

            for (int i = 0; i < wolfEvent.wolfEvents.Count; i++)
            {
                var node = wolfEvent.wolfEvents[i];

                if (node.GetType() == typeof(WolfEventNodeBase))
                {
                    switch (node.name)
                    {
                        default:
                            customEvent.Add(node.name, i);
                            break;
                        case "Start":
                            startEvent.Add(i);
                            break;
                        case "Update":
                            updateEvent.Add(i);
                            break;
                    }
                }
            }
        }

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
                if (targ.wolfEvent == null)
                {
                    //targ.wolfEvent = new List<WolfEventNodeData>();
                }
                var data = ScriptableObject.CreateInstance<WolfEventData>();
                data.name = targ.name+"'s Event";
                //targ.wolfEvent.Add(newSO);
                data.wolfEvents = new List<WolfEventNodeBase>();
                var node1 = ScriptableObject.CreateInstance<WolfEventNodeBase>();
                var node2 = ScriptableObject.CreateInstance<WolfEventNodeBase>();
                var node3 = ScriptableObject.CreateInstance<WolfEventNodeBase>();
                data.wolfEvents.Add(node1);
                data.wolfEvents.Add(node2);
                data.wolfEvents.Add(node3);
                targ.wolfEvent = data;
                EditorUtility.SetDirty(targ);
            }


            if (GUILayout.Button("Open Editor Window", GUILayout.Height(29)))
            {
                var w = WolfEventGraphWindow.OpenWolfEventGraphWindow();
                w.LoadEvents(targ);
            }

            serializedObject.ApplyModifiedProperties();
        }

    }
#endif

}