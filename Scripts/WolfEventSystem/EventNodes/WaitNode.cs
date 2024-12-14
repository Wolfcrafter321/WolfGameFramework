using System.Collections;
using UnityEngine;
using Wolf;

namespace WolfEventNode
{
    [WolfEventNode, NodeSearchPath("Event/Wait")]
    public class WaitNode : WolfEventNodeBase
    {

        public ConnectableVariableFloat waitTime;

        public override IEnumerator ProcessEvent(WolfEventData source)
        {
            yield return new WaitForSeconds((float)waitTime.GetValue(source));
            yield return base.ProcessEvent(source);
        }
    }
}
