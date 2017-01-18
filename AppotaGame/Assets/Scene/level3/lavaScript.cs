using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class lavaScript : MonoBehaviour {

    //public Vector2 range = new Vector2(0.1f, 1);
    //public float speed = 1;
    float[] randomTimes;
    Mesh mesh;
    float midY_value;
    Vector3[] verts;
    lavaPointScript[] Points;
    public float max;
    public float min;
    bool movingUp;
    float timer = 0;



    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        verts = mesh.vertices;
    }

    void Update()
    {
        
        for (int i = 0; i < verts.Length; i++)
        {
            if ((i + 1) % 4 == 0)
            {
                if (movingUp)
                {
                    verts[i] += new Vector3( 0, 0.001f, 0);
                }
                else
                {
                    verts[i] += new Vector3( 0, -0.001f, 0);
                }
            }
            else if((i + 1) % 2 == 0)
            {
                if (movingUp)
                {
                    verts[i] += new Vector3(0,  0.001f, 0);
                }
                else
                {
                    verts[i] += new Vector3(0,  -0.001f, 0);
                }
            }

            else if ((i + 1) % 3 == 0)
            {
                if (movingUp)
                {
                    verts[i] += new Vector3(0,  -0.001f, 0);
                }
                else
                {
                    verts[i] += new Vector3(0,  0.001f, 0);
                }
            }
            else
            {
                if (movingUp)
                {
                    verts[i] += new Vector3(0,  -0.001f,0);
                }
                else
                {
                    verts[i] += new Vector3(0,  0.001f,0);
                }
            }
        }

        //verts[0] += new Vector3(0, 0, 0.0001f);
        //verts[1] += new Vector3(0, 0, 0.0001f);
        //verts[2] += new Vector3(0, 0, 0.0001f);
        //verts[3] += new Vector3(0, 0, 0.0001f);

        //verts[4] += new Vector3(0, 0, 0.0001f);
        //verts[5] += new Vector3(0, 0, 0.0001f);
        //verts[6] += new Vector3(0, 0, 0.0001f);
        //verts[7] += new Vector3(0, 0, 0.0001f);


        //verts[8] += new Vector3(0, 0, 0.0001f);
        //verts[9] += new Vector3(0, 0, 0.0001f);
        //verts[10] += new Vector3(0, 0, 0.0001f);
        //verts[11] += new Vector3(0, 0, 0.0001f);

        //verts[12] += new Vector3(0, 0, 0.0001f);
        //verts[13] += new Vector3(0, 0, 0.0001f);
        //verts[14] += new Vector3(0, 0, 0.0001f);
        //verts[15] += new Vector3(0, 0, 0.0001f);

        //verts[16] += new Vector3(0, 0, 0.0001f);
        //verts[17] += new Vector3(0, 0, 0.0001f);
        //verts[18] += new Vector3(0, 0, 0.0001f);
        //verts[19] += new Vector3(0, 0, 0.0001f);

        //verts[20] += new Vector3(0, 0, 0.0001f);
        //verts[21] += new Vector3(0, 0, 0.0001f);
        //verts[22] += new Vector3(0, 0, 0.0001f);
        //verts[23] += new Vector3(0, 0, 0.0001f);

        //verts[4] += new Vector3(0, 0, 0.0001f);
        mesh.vertices = verts;
        //int i = 0;
        //while (i < verts.Length)
        //{
        //    if (IsOdd(i))
        //    {
        //        if (movingUp)
        //        {
        //            verts[i] += new Vector3(0, 0, 0.0001f);
        //        }
        //        else
        //        {
        //            verts[i] += new Vector3(0, 0, -0.0001f);
        //        }

        //    }
        //    else
        //    {
        //        if (movingUp)
        //        {
        //            verts[i] += new Vector3(0, 0, -0.0001f);
        //        }
        //        else
        //        {
        //            verts[i] += new Vector3(0, 0, 0.0001f);
        //        }

        //    }
        //    i++;
        //}
        //mesh.vertices = verts;
        //mesh.RecalculateBounds();

        timer += Time.deltaTime;
        if (timer > 2.0)
        {
            if (movingUp)
            {
                movingUp = false;
            }
            else
            {
                movingUp = true;
            }
            timer = 0;
        }

    }



    private bool IsOdd(int value)
    {
        return value % 2 != 0;
    }
    //void Update()
    //{

    //    List<Vector3> newVerts = null;
    //    Debug.Log("HAHAHAHAHAHA;" + Points[0].GetYValue());
    //    foreach (lavaPointScript p in Points)
    //    {
    //        if (p.MovingUp())
    //        {
    //            if (p.GetYValue() > max)
    //            {
    //                p.MoveVectorDown();
    //                p.SetMovingUp(false);
    //            }
    //            else
    //            {
    //                p.MoveVectorUp();
    //            }
    //        }
    //        else if (!p.MovingUp())
    //        {
    //            if (p.GetYValue() < min)
    //            {
    //                p.MoveVectorUp();
    //                p.SetMovingUp(false);
    //            }
    //            else
    //            {
    //                p.MoveVectorDown();
    //            }
    //        }

    //        newVerts.Add(p.GetVert());
    //    }


    //    Debug.Log("JEG KOM IGENNEM");
    //    mesh.vertices = newVerts.ToArray();
    //    mesh.RecalculateBounds();
    //}


    //void GetMidAxisOfVerts()
    //{
    //    float totalVertsY_value = 0;
    //    int tik = 0;
    //    foreach (Vector3 vect in verts)
    //    {
    //        lavaPointScript lp = new lavaPointScript();
    //        lp.SetVert(vect);
    //        float valueY = vect.y;

    //        lp.SetYValue(valueY);
    //        Debug.Log("AHAHHAHAH"+ lp.GetYValue());
    //        totalVertsY_value += valueY;

    //        Points[tik] = lp;
    //        tik++;
    //    }

    //    midY_value = (totalVertsY_value / verts.Length);
    //    foreach(lavaPointScript p in Points)
    //    {
    //        if(p.GetYValue() > midY_value)
    //        {
    //            p.SetMovingUp(true);
    //        }
    //        else
    //        {
    //            p.SetMovingUp(false);
    //        }
    //    }
    //}




    //void Start()
    //{
    //    mesh = GetComponent<MeshFilter>().mesh;
    //    int i = 0;
    //    randomTimes = new float[mesh.vertices.Length];

    //    while (i < mesh.vertices.Length)
    //    {
    //        randomTimes[i] = Random.Range(range.x, range.y);

    //        i++;
    //    }

    //}

    //void Update()
    //{
    //    mesh = GetComponent<M eshFilter>().mesh;
    //    Vector3[] vertices = mesh.vertices;
    //    Vector3[] normals = mesh.normals;
    //    int i = 0;
    //    while (i < vertices.Length)
    //    {
    //        vertices[i].z = 1 * Mathf.PingPong(Time.time * speed, randomTimes[i]);
    //        i++;
    //    }
    //    mesh.vertices = vertices;
    //}
}
