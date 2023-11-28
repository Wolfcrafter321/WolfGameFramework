using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Wolf/Debug/GLLine")]
public class GLLine : MonoBehaviour
{


    //public Vector3 aa;
    public Material mat;

    public List<Vec32> lines;
    public List<Cub32> cubes;
    public List<Grid32> grids;

    void OnRenderObject()
    {
        if(mat != null)
        {
            mat.SetPass(0);

            GL.PushMatrix();
            GL.Begin(GL.LINES);

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].enable)
                {
                    GL.Color(lines[i].col);
                    GL.Vertex3(lines[i].a.x, lines[i].a.y, lines[i].a.z);
                    GL.Vertex3(lines[i].b.x, lines[i].b.y, lines[i].b.z);
                }
            }

            for (int i = 0; i < cubes.Count; i++)
            {
                if (cubes[i].enable)
                {
                    float scl = cubes[i].scale;
                    Vector3 po = transform.position;
                    Vector3 pos = cubes[i].pos;
                    float x = po.x + pos.x;
                    float y = po.y + pos.y;
                    float z = po.z + pos.z;
                    GL.Color(cubes[i].col);
                    GL.Vertex3(x - scl, y - scl, z - scl);
                    GL.Vertex3(x - scl, y - scl, z + scl);

                    GL.Vertex3(x - scl, y - scl, z + scl);
                    GL.Vertex3(x + scl, y - scl, z + scl);

                    GL.Vertex3(x + scl, y - scl, z + scl);
                    GL.Vertex3(x + scl, y - scl, z - scl);

                    GL.Vertex3(x + scl, y - scl, z - scl);
                    GL.Vertex3(x - scl, y - scl, z - scl);

                    GL.Vertex3(x - scl, y + scl, z - scl);
                    GL.Vertex3(x - scl, y + scl, z + scl);

                    GL.Vertex3(x - scl, y + scl, z + scl);
                    GL.Vertex3(x + scl, y + scl, z + scl);

                    GL.Vertex3(x + scl, y + scl, z + scl);
                    GL.Vertex3(x + scl, y + scl, z - scl);

                    GL.Vertex3(x + scl, y + scl, z - scl);
                    GL.Vertex3(x - scl, y + scl, z - scl);


                    GL.Vertex3(x - scl, y - scl, z - scl);
                    GL.Vertex3(x - scl, y + scl, z - scl);

                    GL.Vertex3(x - scl, y - scl, z + scl);
                    GL.Vertex3(x - scl, y + scl, z + scl);

                    GL.Vertex3(x + scl, y - scl, z - scl);
                    GL.Vertex3(x + scl, y + scl, z - scl);

                    GL.Vertex3(x + scl, y - scl, z + scl);
                    GL.Vertex3(x + scl, y + scl, z + scl);
                }
            }


            for (int i = 0; i < grids.Count; i++)
            {
                if (grids[i].enable)
                {

                    float size = grids[i].scale;
                    int count = grids[i].chunks/2;
                    GL.Color(grids[i].col);

                    for (int j = -count; j < count; j++)
                    {
                        for (int k = -count; k < count; k++)
                        {
                            Vector3 pos = new Vector3(j, 0, k) * size;
                            GL.Vertex3(pos.x, 0, pos.z);
                            GL.Vertex3(pos.x + size, 0, pos.z);
                            GL.Vertex3(pos.x, 0, pos.z);
                            GL.Vertex3(pos.x, 0, pos.z + size);

                        }
                    }
                }
            }


            GL.End();
            GL.PopMatrix();

        }
    }

    public void DrawLine(Vector3 posA, Vector3 posB)
    {
        Vec32 n = new Vec32();
        n.a = posA;
        n.enable = true;
        n.b = posB;
        n.col = Color.white;
        lines.Add(n);
    }
    public void DrawLine(Vector3 posA, Vector3 posB, Color col)
    {
        Vec32 n = new Vec32();
        n.enable = true;
        n.a = posA;
        n.b = posB;
        n.col = col;
        lines.Add(n);
    }

    public void ClearLine()
    {
        lines.Clear();
    }

    [System.Serializable]
    public struct Vec32
    {
        public bool enable;
        public Color col;
        public Vector3 a;
        public Vector3 b;
    }

    [System.Serializable]
    public struct Cub32
    {
        public bool enable;
        public Color col;
        public Vector3 pos;
        public float scale;
    }

    [System.Serializable]
    public struct Grid32
    {
        public bool enable;
        public Color col;
        public float scale;
        [Range(0, 500)]
        public int chunks;
    }

}
