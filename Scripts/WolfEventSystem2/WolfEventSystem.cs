using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace WolfEventSystem2
{
    public class WolfEventSystem2 : MonoBehaviour
    {

    }


#if UNITY_EDITOR
    [CustomEditor(typeof(WolfEventSystem2))]
    public class WolfEventSystem2Editor : Editor
    {

        private WolfEventSystem2 targ;

        public override void OnInspectorGUI()
        {s
            targ = this.target as WolfEventSystem2;

            base.OnInspectorGUI();

            if (GUILayout.Button("CreateEventSO"))
            {
            }

            if (GUILayout.Button("Open Editor Window", GUILayout.Height(29)))
            {
            }

            if (GUILayout.Button("Debug", GUILayout.Height(29)))
            {
            }

            if (GUILayout.Button("Debug InvokeFrom0", GUILayout.Height(29)))
            {
            }

            serializedObject.ApplyModifiedProperties();
        }

    }
#endif

}