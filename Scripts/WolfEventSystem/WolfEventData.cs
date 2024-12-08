using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolf;


#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;
using System.Reflection;
#endif


namespace Wolf
{
    [System.Serializable]
    [CreateAssetMenu(fileName ="New Event", menuName ="Wolf/WolfEventNodeData")]
    public class WolfEventData : ScriptableObject
    {

        public List<WolfEventNodeBase> wolfEvents;

        [HideInInspector] private WolfEventNodeBase _currentEvent;
        [HideInInspector] public WolfEventNodeBase currentEvent
        {
            get { return _currentEvent; }
            set { _currentEvent = value; _currentEvent.onBecomeTarget?.Invoke(); }
        }


        public void StartEventAt(int index, WolfEventSystem parentGameObject)
        {
            StartEventAt(parentGameObject, index);
        }
        public void StartEventAt(WolfEventSystem parentGameObject, int index)
        {
            currentEvent = wolfEvents[index];
            parentGameObject.StartCoroutine(StartEventCoroutine());
        }

        IEnumerator StartEventCoroutine()
        {
            while (currentEvent != null)
            {
                yield return currentEvent.ProcessEvent(this);
            }
        }

    }



#if UNITY_EDITOR
    [CustomEditor(typeof(WolfEventData))]
    public class WolfEventNodeDataEditor : Editor
    {

        private WolfEventData targ;

        public override void OnInspectorGUI()
        {
            targ = this.target as WolfEventData;

            if (GUILayout.Button("CreateEvent - Base"))
            {
                var data = ScriptableObject.CreateInstance<WolfEventNodeBase>();
                data.name = Guid.NewGuid().ToString();
                targ.wolfEvents.Add(data);
                data.position = new Vector2(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100));
                EditorUtility.SetDirty(targ);
            }

            if (GUILayout.Button("Open Editor Window", GUILayout.Height(29)))
            {
                var w = WolfEventGraphWindow.OpenWolfEventGraphWindow();
                w.LoadEvents(targ);
            }

            base.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
        }

    }
#endif

}