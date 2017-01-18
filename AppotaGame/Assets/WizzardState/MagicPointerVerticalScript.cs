using UnityEngine;
using System.Collections;

public class MagicPointerVerticalScript : MonoBehaviour {

    
    private bool movingUp;
    private GameObject wizzard;


    void Start()
    {
        wizzard = GameObject.Find("wizzard");
        movingUp = true;
    }
    
    void Update()
    {
        if (wizzard.GetComponent<MagicController>().wizzardState == WizzardState.vertical)
        {
            Vector3 Rotation = gameObject.transform.rotation.eulerAngles;
            if ( movingUp)
            {
                transform.Rotate(0, -1.5f,  0);
                if (Rotation.x < 340 && Rotation.x > 320 && Rotation.y > 200)
                {
                    movingUp = false;
                }
            }
            else if ( !movingUp)
            {
                transform.Rotate(0, 1.5f, 0);
                if (Rotation.x > 320 && Rotation.x < 340 && Rotation.y < 140)
                {
                    movingUp = true;
                }
            }
        }
    }
}
