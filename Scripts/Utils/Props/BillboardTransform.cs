using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [AddComponentMenu("Wolf/Utils/Props/BillboardTransforms")]
    public class BillboardTransform : MonoBehaviour
    {
        public Transform target;
        void Update()
        {
            Transform theTarget =
                (target != null) ? target :
                (Camera.current != null) ? Camera.current.transform : Camera.main.transform ;
            if(theTarget != null) transform.LookAt(theTarget);
        }
    }
}