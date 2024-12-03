using UnityEngine;

namespace Wolf{
    [AddComponentMenu("Wolf/Utils/Props/SimpleCurveAnimator")]
    public class SimpleCurveAnimator : MonoBehaviour
    {


        public bool p_x;
        public bool p_y;
        public bool p_z;
        [Space]
        public bool r_x;
        public bool r_y;
        public bool r_z;
        [Space]
        public bool s_x;
        public bool s_y;
        public bool s_z;

        [Space]
        public float totalTime;

        [Space]
        public AnimationCurve p_x_c;
        public AnimationCurve p_y_c;
        public AnimationCurve p_z_c;
        [Space]
        public AnimationCurve r_x_c;
        public AnimationCurve r_y_c;
        public AnimationCurve r_z_c;
        [Space]
        public AnimationCurve s_x_c;
        public AnimationCurve s_y_c;
        public AnimationCurve s_z_c;

        private float currentTime;

        void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime > totalTime) currentTime = 0;


            Vector3 p = transform.position;
            Vector3 r = transform.rotation.eulerAngles;
            Vector3 s = transform.localScale;

            if (p_x) p.x = p_x_c.Evaluate(currentTime);
            if (p_y) p.y = p_y_c.Evaluate(currentTime);
            if (p_z) p.z = p_z_c.Evaluate(currentTime);

            if (r_x) r.x = r_x_c.Evaluate(currentTime);
            if (r_y) r.y = r_y_c.Evaluate(currentTime);
            if (r_z) r.z = r_z_c.Evaluate(currentTime);

            if (s_x) s.x = s_x_c.Evaluate(currentTime);
            if (s_y) s.y = s_y_c.Evaluate(currentTime);
            if (s_z) s.z = s_z_c.Evaluate(currentTime);

            transform.position = p;
            transform.eulerAngles = r;
            transform.localScale = s;
        }
    }
}