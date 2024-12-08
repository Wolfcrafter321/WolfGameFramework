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

    [System.Serializable]
    public abstract class WolfEventConnectableVariableBase {
        public abstract object GetValue(WolfEventData data);

        public abstract connectionInfo GetInputConnectionInfo();
        public abstract connectionInfo GetOutputConnectionInfo();

        [System.Serializable]
        public struct connectionInfo
        {
            public int targNode;
            public int targSlot;
        }

    }

    [System.Serializable]
    public class WolfEventConnectableVariable<T> : WolfEventConnectableVariableBase
    {
        public WolfEventConnectableVariable(T val){
            value = val;
            inputSideConnection = new connectionInfo { targNode = -1, targSlot = -1 };
            outputSideConnection = new connectionInfo { targNode = -1, targSlot = -1 };
        }

        public T value;

        public connectionInfo inputSideConnection;
        public connectionInfo outputSideConnection;

        public override object GetValue(WolfEventData data)
        {
            if(inputSideConnection.targNode != -1)
            {
                return data.wolfEvents[inputSideConnection.targNode].GetValueAt(data, inputSideConnection.targSlot);
            }
            return value;
        }

        public override connectionInfo GetInputConnectionInfo() { return inputSideConnection; }
        public override connectionInfo GetOutputConnectionInfo() { return outputSideConnection; }

    }
}