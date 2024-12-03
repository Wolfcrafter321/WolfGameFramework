using UnityEngine;

namespace Wolf
{
    [AddComponentMenu("Wolf/Utils/Props/SimpleRotation")]
    public class simplerotation : MonoBehaviour
    {

        public float spd_x;
        public float spd_y;
        public float spd_z;

        public bool gizmo;

        void Start()
        {
            gizmo = false;
        }

        void Update()
        {
            Vector3 v = new Vector3(spd_x, spd_y, spd_z);
            transform.Rotate(v * Time.deltaTime);
        }

        void OnDrawGizmos()
        {
            if (gizmo)
            {
                Vector3 v = new Vector3(spd_x, spd_y, spd_z);
                transform.Rotate(v * Time.deltaTime);
            }
        }
    }
}
