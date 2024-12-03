using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Wolf
{
    [AddComponentMenu("Wolf/Game/DummyGameFlows")]
    public class DummyGameFlow : MonoBehaviour
    {

        public string[] flows;

        IEnumerator Start()
        {
            yield return null;

            for (int i = 0; i != flows.Length; i++)
            {
                Debug.Log(flows[i]);
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }
}