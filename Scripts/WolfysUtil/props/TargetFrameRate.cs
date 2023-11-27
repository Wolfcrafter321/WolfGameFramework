using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{

    public int targetFrameRate;

    private void Start()
    {
        SetFrameRate();
    }

    public void SetFrameRate(int _v){
        
        Application.targetFrameRate = _v;
    }
    [ContextMenu("Set")]
    public void SetFrameRate(){
        
        Application.targetFrameRate = targetFrameRate;
    }
}
