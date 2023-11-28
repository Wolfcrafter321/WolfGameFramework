using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Wolf/Utils/Props/LookAt")]
public class LookAt : MonoBehaviour
{

    public Transform aimtarget;

    void Update()
    {
        transform.LookAt(aimtarget);
    }
}
