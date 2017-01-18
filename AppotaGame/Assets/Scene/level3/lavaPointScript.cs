using UnityEngine;
using System.Collections;

public class lavaPointScript {

    bool movingUp;
    Vector3 vert;
    float YValue;

    public bool MovingUp()
    {
        return movingUp;
    }
    public void SetMovingUp(bool ans)
    {
        this.movingUp = ans;
    }

    public void SetVert(Vector3 v)
    {
        this.vert = v;
    }
    public Vector3 GetVert()
    {
        return vert;
    }
    public void MoveVectorUp()
    {
        vert.Set(0, vert.y + 1, 0);
    }
    public void MoveVectorDown()
    {
        vert.Set(0, vert.y - 1, 0);
    }

    public void SetYValue(float y)
    {
        this.YValue = y;
    }
    public float GetYValue()
    {
        return YValue;
    }

}
