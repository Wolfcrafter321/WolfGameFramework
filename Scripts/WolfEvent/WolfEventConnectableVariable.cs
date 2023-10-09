using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [System.Serializable]
    public class WolfEventConnectableVariable<T>
    {
        public WolfEventSO sourceEvent;
        public int sourceTarget;
        public T value
        {
            get {
                if (sourceTarget == -1)
                {
                    return value;
                }
                else{
                    var n = (IWolfEventVariable)sourceEvent.wolfEvents[sourceTarget];
                    return (T)n.GetValue();
                }
            }
            set { }
        }

        public void Init(WolfEventSO sourceSO, T value, int sourceTarget = -1)
        {
            this.sourceEvent = sourceSO;
            this.value = value;
            this.sourceTarget = sourceTarget;
        }

    }
}