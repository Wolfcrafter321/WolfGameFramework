using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [AddComponentMenu("Wolf/Utils/Props/CopyTransform")]
    public class CopyTransform : MonoBehaviour
    {


        public Transform targetPosition;

        public bool copyPosX;
        public bool copyPosY;
        public bool copyPosZ;
        public bool copyRotX;
        public bool copyRotY;
        public bool copyRotZ;

        void Update()
        {
            transform.position = new Vector3(
                (copyPosX) ? targetPosition.position.x : transform.position.x,
                (copyPosY) ? targetPosition.position.y : transform.position.y,
                (copyPosZ) ? targetPosition.position.z : transform.position.z
                );
            transform.eulerAngles = new Vector3(
                (copyRotX) ? targetPosition.eulerAngles.x : transform.eulerAngles.x,
                (copyRotY) ? targetPosition.eulerAngles.y : transform.eulerAngles.y,
                (copyRotZ) ? targetPosition.eulerAngles.z : transform.position.z
                );
        }
    }
}
