using UnityEngine;
using Wolf;

namespace WolfEventNode
{
    [WolfEventVariableNode, NodeSearchPath("Calculate/Int/Add")]
    public class IntAddNode : WolfEventNodeBase
    {
        public ConnectableVariableString a;
        public ConnectableVariableString b;
    }
}