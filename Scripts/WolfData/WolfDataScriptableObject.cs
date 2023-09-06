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

        public List<object> datas;
        //public Dictionary<string, object> datas;

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

            if (data.datas == null) data.datas = new List<object>();
            //if(data.datas == null) data.datas = new Dictionary<string, object>();

            base.OnInspectorGUI();

            for (int i = 0; i < data.datas.Count; i++)
            //foreach (var key in data.datas.Keys)
            {
                EditorGUILayout.BeginHorizontal();
                //data.datas[key] = EditorGUILayout.ObjectField(data.datas[key] as UnityEngine.Object, typeof(ScriptableObject)) as UnityEngine.Object;
                //data.datas[i] = EditorGUILayout.PropertyField(data.datas[i]);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Base"))
            {
                var v = ScriptableObject.CreateInstance<WolfVariableBase>();
                AddToSubAssetAndVariable(v);
            };
            if (GUILayout.Button("Add Sample"))
            {
                var v = ScriptableObject.CreateInstance<WolfVariableSampleClass>();
                AddToSubAssetAndVariable(v);
            };
        }

        void AddToSubAssetAndVariable(UnityEngine.Object v)
        {
            v.name = GUID.Generate().ToString();
            data.datas.Add(v);
            AssetDatabase.AddObjectToAsset(v, data);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
#endif

}