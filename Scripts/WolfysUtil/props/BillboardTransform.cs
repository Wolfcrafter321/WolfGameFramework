using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    public class BillboardTransform : MonoBehaviour
    {
        public Transform target;
        void Update()
        {
            transform.LookAt((target != null) ? target : Camera.current.transform);
        }
    }
}