using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif
namespace Wolf
{
    [System.Serializable]
    public class WolfVariableBase : ScriptableObject
    {
        public new string name = "var";
        public int value;
        public int min = 0;
        public int max = 100;
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(WolfVariableBase))]
    [CanEditMultipleObjects()]
    public class WolfVariableBaseEditor : Editor
    {
        WolfVariableBase data;
        bool folding = false;

        public override void OnInspectorGUI()
        {
            data = (WolfVariableBase)this.target;

            //base.OnInspectorGUI();
            data.name = EditorGUILayout.TextField("name", data.name);
            data.value = EditorGUILayout.IntSlider("value", data.value, data.min, data.max);
            if(folding = EditorGUILayout.Foldout(folding, "Editor"))
            {
                EditorGUILayout.BeginHorizontal();
                data.min = EditorGUILayout.IntField("min", data.min);
                data.max = EditorGUILayout.IntField("max", data.max);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
#endif
}