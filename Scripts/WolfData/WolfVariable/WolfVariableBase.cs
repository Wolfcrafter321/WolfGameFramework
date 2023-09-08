using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif
namespace Wolf
{
    [System.Serializable]
    public class WolfVariableBase
    {
        public string displayName = "var";
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(WolfVariableBase))]
    [CanEditMultipleObjects()]
    public class WolfVariableBaseEditor : Editor
    {
        WolfVariableBase data;
        public override void OnInspectorGUI()
        {
            
        }
    }
#endif
}