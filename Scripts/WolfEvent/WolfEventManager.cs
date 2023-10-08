using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Wolf
{
    public class WolfEventManager : MonoBehaviour
    {

        public List<WolfEventSO> wolfEventSOs;

        [ContextMenu("Hi")]
        void Test()
        {
            Debug.Log("テストです。イベントパックの[0]を実行します。");
            wolfEventSOs[0].StartEvent(this);
        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(WolfEventManager))]
    public class WolfEventManagerEditor : Editor
    {

        private WolfEventManager targ;

        public override void OnInspectorGUI()
        {
            targ = this.target as WolfEventManager;

            base.OnInspectorGUI();

            if (GUILayout.Button("CreateEventSO"))
            {
                targ.wolfEventSOs = new List<WolfEventSO>();
                var newSO = ScriptableObject.CreateInstance<WolfEventSO>();
                newSO.name = "MyEvents1";
                newSO.wolfEvents = new List<WolfEventBase>();
                newSO.wolfEvents.Add(new WolfEventBase());
                newSO.wolfEvents.Add(new WolfEventPrint());
                newSO.wolfEvents.Add(new WolfEventBase());
                newSO.wolfEvents[0].text = "A";
                newSO.wolfEvents[0].targetEvent = 2;
                newSO.wolfEvents[1].text = "C";
                newSO.wolfEvents[1].targetEvent = -1;
                newSO.wolfEvents[2].text = "B";
                newSO.wolfEvents[2].targetEvent = 1;
                targ.wolfEventSOs.Add(newSO);
            }
        }
    }
#endif

    }