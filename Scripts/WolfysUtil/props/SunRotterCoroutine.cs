using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotterCoroutine : MonoBehaviour
{

    public enum Type { DayLenght, RealTime }

    public bool enable = true;
    public float UpdateTime = 1f;
    public Light sun;

    [Space]
    public Type type = Type.DayLenght;
    public float DayLenght;

    public float current=0f;

    [Space]
    public AnimationCurve sunIntensity;

    void Start()
    {
        StartCoroutine("RotMas");
    }

    void Update()
    {
        current += Time.deltaTime * (360/DayLenght);
        if (current >= 360) current = 0;
    }


    IEnumerator RotMas()
    {

        while (enable)
        {
            Rot();
            yield return new WaitForSeconds(UpdateTime);
        }

        yield break;
    }

    void Rot()
    {
        sun.intensity = sunIntensity.Evaluate(current);
        transform.eulerAngles = new Vector3(current, 0, 0);
    }

}
