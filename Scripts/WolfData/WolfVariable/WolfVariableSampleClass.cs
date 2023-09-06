using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif
namespace Wolf
{
    [System.Serializable]
    public class WolfVariableSampleClass : ScriptableObject
    {
        public new string name = "Item";
        public string itemName;
        public int itemCount;
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(WolfVariableSampleClass))]
    public class WolfVariableSampleClassEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
#endif
}