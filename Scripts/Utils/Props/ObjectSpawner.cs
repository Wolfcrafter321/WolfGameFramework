using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    [AddComponentMenu("Wolf/Utils/Props/ObjectSpawner")]
    public class ObjectSpawner : MonoBehaviour
    {

        public Vector3 dir;
        public GameObject obj;
        public float delay;
        /// <summary>
        /// 0 is endless
        /// </summary>
        [Min(0)]
        public int time;
        public bool addRB;

        private float cdelay;
        private int ctime;

        void Update()
        {
            cdelay += Time.deltaTime;
            if (cdelay > delay && (time == 0 || ctime < time))
            {
                Spawn();
                cdelay = 0;
                ctime++;
            }
        }

        void Spawn()
        {
            GameObject nobj = Instantiate(obj) as GameObject;
            nobj.transform.position = this.transform.position;
            nobj.transform.rotation = this.transform.rotation;
            Rigidbody rb;
            if (addRB || nobj.TryGetComponent(out rb))
            {
                nobj.AddComponent<Rigidbody>().linearVelocity = dir;
            }
        }

    }
}
