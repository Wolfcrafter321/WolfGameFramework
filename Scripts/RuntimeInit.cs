using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Wolf
{
    public class RuntimeInit : MonoBehaviour
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            var obj = Instantiate(Resources.Load("Manager"));
            DontDestroyOnLoad(obj);

        }

        private void Start()
        {
            Destroy(this.gameObject);
        }
    }
}