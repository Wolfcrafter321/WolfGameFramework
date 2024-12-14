using System.Collections;
using UnityEngine;
using Wolf;

namespace WolfEventNode
{
    [WolfEventNode, NodeSearchPath("Test/PlayAudio")]
    public class SimpleSENode : WolfEventNodeBase
    {
        public ConnectableVariableAudioClip clip;

        public override IEnumerator ProcessEvent(WolfEventData source)
        {
            try
            {
                BasicGameManager.singleton.GetComponent<AudioSource>().PlayOneShot((AudioClip)clip.GetValue(source));
            }
            catch (System.Exception)
            {
                Debug.LogError("SE Node need audiosource. Please add AudioSource to BasicGameManager's Instance");
            }
            yield return base.ProcessEvent(source);
        }

    }
}