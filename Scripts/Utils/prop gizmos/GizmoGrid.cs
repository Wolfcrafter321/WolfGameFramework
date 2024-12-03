using UnityEngine;

namespace Wolf{
    [AddComponentMenu("Wolf/Utils/Props Gizmos/GizmoGrid")]
    public class GizmoGrid : MonoBehaviour
    {

        public bool enable;
        public grid[] grids;
        
        void OnDrawGizmos()
        {
            if (enable)
            {
                for (int i = 0; i < grids.Length; i++)
                {
                    Gizmos.color = grids[i].col;
                    DrawPlane( grids[i].size, grids[i].chunks);
                }
            }
        }
        
        void DrawPlane(float size, int count )
        {
            for (int i = -count; i < count; i++)
            {
                for (int j = -count; j < count; j++)
                {
                    //    Vector3 pos = new Vector3(i, 0, j) * size;
                    //    Vector3 npos = new Vector3(1, 0, 1) * (size / 2) + pos;
                    //    Gizmos.DrawWireCube(transform.position + npos, new Vector3(1,0,1) * size);

                    Vector3 pos = new Vector3(i, 0, j) * size;
                    Gizmos.DrawLine(pos, pos + new Vector3(size, 0, 0));
                    Gizmos.DrawLine(pos, pos + new Vector3(0, 0, size));

                }
            }
        }
        

        [System.Serializable]
        public struct grid
        {
            public float size;
            [Range(0,500)]
            public int chunks;
            public Color col;
        }

    }
}
