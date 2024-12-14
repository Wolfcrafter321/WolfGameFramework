using UnityEngine;
using Wolf;

namespace WolfEventNode
{
    [WolfEventVariableNode, NodeSearchPath("Variables/String")]
    public class StringVariableNode : WolfEventNodeBase
    {
        public ConnectableVariableString value;
    }
}