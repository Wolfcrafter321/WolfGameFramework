using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [AddComponentMenu("Wolf/Utils/Props/LerpTranformer")]
    public class LerpTranformer : MonoBehaviour
    {

        public Transform moveTarget;
        public Transform[] targets;

        public bool lerpPosition = true;
        public bool lerpRotation = true;

        [Range(0, 1)]
        public float mTime;
        [Range(0, 1)]
        public float rTime;
        public int id;

        public bool deltaTime;

        void Update()
        {
            if (lerpPosition) moveTarget.position = Vector3.Lerp(targets[id].position, moveTarget.position, mTime * (deltaTime ? Time.deltaTime : 1));
            if (lerpRotation) moveTarget.rotation = Quaternion.Lerp(targets[id].rotation, moveTarget.rotation, rTime * (deltaTime ? Time.deltaTime : 1));
        }
    }
}