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

        public abstract ConnectionInfo GetInputConnectionInfo();
        public abstract ConnectionInfo GetOutputConnectionInfo();

        [System.Serializable]
        public struct ConnectionInfo
        {
            public int targetNode;
            public int targerSlot;
        }

    }

    [System.Serializable]
    public class WolfEventConnectableVariable<T> : WolfEventConnectableVariableBase
    {
        
        public WolfEventConnectableVariable(string name, object val){
            if(val != null) value = (T)val;
            this.name = name;
            inputSideConnection = new ConnectionInfo { targetNode = -1, targerSlot = -1 };
            outputSideConnection = new ConnectionInfo { targetNode = -1, targerSlot = -1 };
        }

        public string name;
        public T value;

        public ConnectionInfo inputSideConnection;
        public ConnectionInfo outputSideConnection;

        public override object GetValue(WolfEventData data)
        {
            if(inputSideConnection.targetNode != -1)
            {
                return data.wolfEvents[inputSideConnection.targetNode].GetValueAt(data, inputSideConnection.targerSlot);
            }
            return value;
        }

        public override ConnectionInfo GetInputConnectionInfo() { return inputSideConnection; }
        public override ConnectionInfo GetOutputConnectionInfo() { return outputSideConnection; }

    }
}