using UnityEngine;
using Wolf;

namespace WolfEventNode
{
    [WolfEventVariableNode, NodeSearchPath("Variables/Int")]
    public class IntVariableNode : WolfEventNodeBase
    {
        public ConnectableVariableInt value;
    }
}