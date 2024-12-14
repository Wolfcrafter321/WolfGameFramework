using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolf;

namespace Wolf
{
    [System.Serializable]
    public struct ConnectionInfo
    {
        public int targetNode;
        public int targetSlot;
    }

    [System.Serializable]
    public abstract class ConnectableVariableBase : ScriptableObject
    {

        public abstract object GetThisValue();
        public abstract object GetValue(WolfEventData data);
        public abstract void SetValue(object value);

        public abstract ConnectionInfo GetInputConnectionInfo();
        public abstract ConnectionInfo GetOutputConnectionInfo();

    }

    [System.Serializable]
    public class ConnectableVariable<T> : ConnectableVariableBase
    {
        public ConnectableVariable(){
            inputSideConnection = new ConnectionInfo { targetNode = -1, targetSlot = -1 };
            outputSideConnection = new ConnectionInfo { targetNode = -1, targetSlot = -1 };
        }

        public T value;

        public ConnectionInfo inputSideConnection;
        public ConnectionInfo outputSideConnection;

        public override object GetThisValue()
        {
            return value;
        }

        public override object GetValue(WolfEventData data)
        {
            if(inputSideConnection.targetNode != -1)
                return data.wolfEvents[inputSideConnection.targetNode].GetValue(inputSideConnection.targetSlot);
            return value;
        }

        public override void SetValue(object value)
        {
            Debug.Log(value);
            this.value = (T)value;
        }

        public override ConnectionInfo GetInputConnectionInfo() { return inputSideConnection; }
        public override ConnectionInfo GetOutputConnectionInfo() { return outputSideConnection; }
    }

}