using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Wolf
{
    [System.Serializable]
    [CreateAssetMenu(fileName ="New Event", menuName ="Wolf/WolfEventNodeData")]
    public class WolfEventData : ScriptableObject
    {
        public List<WolfEventNodeBase> wolfEvents;

        [HideInInspector]
        public WolfEventNodeBase nextEvent;

        public void StartEventAt(int index, WolfEventSystem parentGameObject)
        {
            StartEventAt(parentGameObject, index);
        }
        public void StartEventAt(WolfEventSystem parentGameObject, int index)
        {
            nextEvent = wolfEvents[0];
            parentGameObject.StartCoroutine(StartEventCoroutine());
        }


        IEnumerator StartEventCoroutine()
        {
            while (nextEvent != null)
            {
                yield return nextEvent.ProcessEvent(this);
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
                targ.wolfEvents.Add(data);
                data.position = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
                EditorUtility.SetDirty(targ);
            }
            if (GUILayout.Button("CreateEvent - Test"))
            {
                var data = ScriptableObject.CreateInstance<WolfEventNodeTest>();
                targ.wolfEvents.Add(data);
                data.position = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
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