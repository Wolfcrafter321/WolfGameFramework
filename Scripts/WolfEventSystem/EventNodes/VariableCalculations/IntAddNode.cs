using UnityEngine;
using Wolf;

namespace WolfEventNode
{
    [WolfEventVariableNode, NodeSearchPath("Calculate/Int/Add")]
    public class IntAddNode : WolfEventNodeBase
    {
        public ConnectableVariableInt a;
        public ConnectableVariableInt b;
        [ConnectableField(noInput:true, noField:true)] public ConnectableVariableInt result;

        public override object GetValue(int index)
        {
            a.GetValue();
        }
    }
}