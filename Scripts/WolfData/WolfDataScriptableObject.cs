using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif

namespace Wolf
{
    [CreateAssetMenu(fileName = "WolfData", menuName = "Wolf/WolfDataAsset", order = 1), System.Serializable]
    public class WolfDataScriptableObject : ScriptableObject
    {
        public string dataName;
        public bool isSaveable;

        [SerializeField]
        public List< WolfVariableBase> datas;
        //public Dictionary<string, WolfVariableBase> datas;


        void ToSaveableData()
        {
            // here for savedata.
        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(WolfDataScriptableObject))]
    public class WolfDataScriptableObjectEditor : Editor
    {
        WolfDataScriptableObject data;


        public override void OnInspectorGUI()
        {
            data = (WolfDataScriptableObject)target;

            //if (data.datas == null) data.datas = new Dictionary<string, WolfVariableBase>();
            if (data.datas == null) data.datas = new List<WolfVariableBase>();

            base.OnInspectorGUI();

            //var keys = new List<string>(data.datas.Keys);
            //var vals = new List<WolfVariableBase>(data.datas.Values);



            if (GUILayout.Button("Add Base"))
            {
                data.datas.Add(new WolfVariableBase());
            };
            if (GUILayout.Button("Add Sample"))
            {
                data.datas.Add(new WolfVariableSampleClass());
            };
            if (GUILayout.Button("Reset"))
            {
                data.datas = new List<WolfVariableBase>();
            };

            EditorGUILayout.LabelField("fwhfhreuif");
            for (int i = 0; i < data.datas.Count; i++)
            //foreach (var key in data.datas.Keys)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("aa");
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.LabelField("fwhfhreuif");


        }

        void AddToSubAssetAndVariable(WolfVariableBase v)
        {
            //data.datas.Add(v.name, v);
            data.datas.Add(v);
        }
    }
#endif

}