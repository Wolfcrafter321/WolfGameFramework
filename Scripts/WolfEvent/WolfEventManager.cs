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

        public WolfEventPrint pr;

        [ContextMenu("Hi")]
        void Test()
        {
            if(wolfEventSOs != null) wolfEventSOs[0].StartEvent(this);
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
                var newSO = ScriptableObject.CreateInstance<WolfEventSO>();     // ToDo: エディター上の新規作成はシリアライズの問題か、上手くいかない。
                newSO.name = "MyEvents1";
                newSO.wolfEvents = new List<WolfEventBase>();                   // 実行時に新規作成すれば問題ないが、それではエディタ上での保存ができない。
                var A = CreateInstance<WolfEventBase>();
                var B = CreateInstance<WolfEventPrint>();
                var C = CreateInstance<WolfEventBase>();
                var D = CreateInstance<WolfEventVariableString>();
                var E = CreateInstance<WolfEventDebugLog>();
                A.targetEvent = 2;
                B.targetEvent = -1;
                C.targetEvent = 4;
                D.targetEvent = -1;
                E.targetEvent = 1;
                B.count = 10;
                B.text_test = new WolfEventConnectableVariable<string>();
                B.text_test.Init(newSO, "", 3);
                B.text_test.sourceTarget = 3;
                D.value = "Test Success!";
                E.text = "Please!";
                newSO.wolfEvents.Add(A);
                newSO.wolfEvents.Add(B);
                newSO.wolfEvents.Add(C);
                newSO.wolfEvents.Add(D);
                newSO.wolfEvents.Add(E);
                targ.wolfEventSOs.Add(newSO);
            }

            if (GUILayout.Button("Open Editor Window", GUILayout.Height(29) ))
            {
                var w = WolfEventGraphWindow.OpenWolfEventGraphWindow();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

    }