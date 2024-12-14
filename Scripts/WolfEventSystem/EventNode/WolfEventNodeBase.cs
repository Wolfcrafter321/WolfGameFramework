using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolf;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;
using System.Reflection;
#endif

namespace Wolf
{
    [Serializable]
    public class WolfEventNodeBase : ScriptableObject
    {


        //public delegate void OnBecomeTarget();
        //public OnBecomeTarget onBecomeTarget;
        public delegate void OnStartEvent();
        public OnStartEvent onStartEvent;
        public delegate void OnEndEvent();
        public OnEndEvent onEndEvent;

        [Header("node")]
        public Vector2 position;

        public int[] targetEvent = new int[1] { -1 };

        public virtual IEnumerator ProcessEvent(WolfEventData source)
        {
            if (targetEvent[0] == -1)
                source.currentEvent = null;
            else
                source.currentEvent = source.wolfEvents[targetEvent[0]];
            yield return null;

            onEndEvent?.Invoke();
        }

        private FieldInfo[] fields;
        public virtual object GetValue(int index)
        {
            if(fields == null) fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                    .Where(type => type.ToString().Contains("Wolf.Connectable")).ToArray();
            index = Mathf.Clamp(index, 0, fields.Length);
            var targFieldInst = fields[index].GetValue(this); // connectableVar
            var targConnectionInfo = targFieldInst.GetType().GetField("inputSideConnection").GetValue(targFieldInst);
            var targN = targConnectionInfo.GetType().GetField("targetNode").GetValue(targConnectionInfo);
            var targS = targConnectionInfo.GetType().GetField("targetSlot").GetValue(targConnectionInfo);
            var val = targFieldInst.GetType().GetField("value").GetValue(targFieldInst);
            return val;
        }


        public void InitFields(object inst)
        {
            var fs = inst.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                    .Where(type => type.ToString().Contains("Wolf.Connectable")).ToArray();
            foreach (var f in fs)
            {
                var insf = ScriptableObject.CreateInstance(f.FieldType);
                f.SetValue(this, insf);
            }
        }


    }


    [AttributeUsage(AttributeTargets.Class)] public class NodeSearchPathAttribute : Attribute{
        public string Path { get; } public NodeSearchPathAttribute(string path) { Path = path; } }

    [AttributeUsage(AttributeTargets.Class, Inherited = true)] public class WolfEventNodeAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Class, Inherited = true)] public class WolfEventFunctionNodeAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Class, Inherited = true)] public class WolfEventVariableNodeAttribute : Attribute { }

}