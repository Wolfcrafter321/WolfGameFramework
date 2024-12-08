using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolf;


#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;
using System.Reflection;
#endif

namespace Wolf
{

    /// <summary>
    /// イベントの根底クラス。
    /// 必要な実装は、virtual IEnumerator ProcessEventと、 public static string searchTreePathの２つを
    /// かならず記述してください。
    /// 現在ノード作成時のフィールドを自動でできないか開発中。
    /// </summary>
    [EventNode]
    public class WolfEventNodeBase : ScriptableObject
    {
        public delegate void OnBecomeTarget();
        public OnBecomeTarget onBecomeTarget;

        [Header("node")]
        public Vector2 position;
        public static string searchTreePath = "Base";


        public int targetEvent = -1;


        [SerializeField]
        public List<WolfEventConnectableVariableBase> values = new List<WolfEventConnectableVariableBase>
        {
            new WolfEventConnectableVariable<string>("BASE")
        };

        /// <summary>
        /// オーバーライドすることで、イベントの挙動を記述できます。
        /// オーバーライド先では、かならず最後にyield return base.ProcessEvent(source);を書いてください。
        /// 書くことで、次のイベントを実行できます。
        /// </summary>
        public virtual IEnumerator ProcessEvent(WolfEventData source)
        {
            if (targetEvent == -1)
                source.currentEvent = null;
            else
                source.currentEvent = source.wolfEvents[targetEvent];
            yield return null;
        }

        public object GetValueAt(WolfEventData data, int slot)
        {
            return values[slot].GetValue(data);
        }

    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true)]public class NodeAttribute : System.Attribute { }
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]public class EventNodeAttribute : System.Attribute { }
    [AttributeUsage(AttributeTargets.Class, Inherited = true)] public class FunctionNodeAttribute : System.Attribute { }
    [AttributeUsage(AttributeTargets.Class, Inherited = true)] public class VariableNodeAttribute : System.Attribute { }

    [AttributeUsage(AttributeTargets.Field)] public class NodeConnectableFieldAttribute : System.Attribute { }
    [AttributeUsage(AttributeTargets.Field)] public class NodeFieldAttribute : System.Attribute { }
    [AttributeUsage(AttributeTargets.Field)]public class InputPortAttribute : System.Attribute{ }
    [AttributeUsage(AttributeTargets.Field)]public class OutputPortAttribute : System.Attribute{ }

    public static class WolfEventNodeAttributeNames
    {
        public const string NodeAttribute                   = "Wolf." + "NodeAttribute";
        public const string EventNodeAttribute              = "Wolf." + "EventNodeAttribute";
        public const string FunctionNodeAttribute           = "Wolf." + "FunctionNodeAttribute";
        public const string VariableNodeAttribute           = "Wolf." + "VariableNodeAttribute";
        public const string NodeConnectableFieldAttribute   = "Wolf." + "NodeConnectableFieldAttribute";
        public const string NodeFieldAttribute              = "Wolf." + "NodeFieldAttribute";
        public const string InputPortAttribute              = "Wolf." + "InputPortAttribute";
        public const string OutputPortAttribute             = "Wolf." + "OutputPortAttribute";
    }

}