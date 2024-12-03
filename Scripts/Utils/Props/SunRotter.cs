using UnityEngine;

[AddComponentMenu("Wolf/Utils/Props/SunRotter")]
public class SunRotter : MonoBehaviour
{

    public enum SunRotationType { Test = 0, None, RealTime, GameTime }

    /// <summary>
    /// Type of rotation. Realtime is Dupricated!!!!!!!
    /// </summary>
    public SunRotationType type;
    public AnimationCurve lightPowerCurve;
    public Gradient sunColorGradient;
    public Gradient ambientColorGradient;

    public int dayCycleTime = 300;

    [Space]
    public Light lightSource;

    [HideInInspector]
    public float currentRawTime;

    void FixedUpdate()
    {

        switch (type)
        {
            case SunRotationType.Test:
                //transform.eulerAngles = new Vector3((GetSec(timeStr) * 6), 0, 0);
                //lightSource.intensity = lightPowerCurve.Evaluate((GetSec(timeStr) * 6) / 15);
                //RenderSettings.ambientIntensity = lightPowerCurve.Evaluate((GetSec(timeStr) * 6) / 15);

                break;
            case SunRotationType.None:
                break;
            case SunRotationType.RealTime:
                //float x = (GetHour24(timeStr) * 15) + (GetMin(timeStr) * 0.25f) + (GetSec(timeStr) * (0.25f / 60));
                //if (debugTime) Debug.Log(timeStr + " = " + x);
                //transform.eulerAngles = new Vector3(x, 0, 0);
                //lightSource.intensity = lightPowerCurve.Evaluate(x / 15);
                //RenderSettings.ambientIntensity = lightPowerCurve.Evaluate(x / 15);
                break;
            case SunRotationType.GameTime:
                if (currentRawTime > dayCycleTime) currentRawTime = 0;
                currentRawTime += Time.deltaTime;
                float X = (((currentRawTime) / dayCycleTime) * 360);
                transform.localEulerAngles = new Vector3(X, 0, 0);
                lightSource.intensity = lightPowerCurve.Evaluate(X / 15);
                RenderSettings.ambientIntensity = lightPowerCurve.Evaluate(X / 15);
                lightSource.color = sunColorGradient.Evaluate((X / 360));
                RenderSettings.ambientLight = ambientColorGradient.Evaluate((X / 360));
                break;
        }

    }


    //public void SetRawTime(float rawTime)
    //{
    //    currentRawTime = rawTime;
    //}
    public void SetTimeHour(int hour)
    {
        currentRawTime = hour / 24 * dayCycleTime;
    }
    public void SetGameTimeOffset(float offset)
    {
        currentRawTime = offset;
    }
    public float getCurrentRawTime()
    {
        return currentRawTime;
    }



}
