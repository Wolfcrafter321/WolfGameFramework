using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    public Transform aimtarget;

    void Update()
    {
        transform.LookAt(aimtarget);
    }
}
