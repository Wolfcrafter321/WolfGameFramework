using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif
namespace Wolf
{
    [System.Serializable]
    public class WolfVariableSampleClass : WolfVariableBase
    {
        public int itemMaxCount = 64;
        public int healPower = 30;
        public int value = 100;
        public int sell = 20;
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