using UnityEngine;
using System.Collections;

public class MagicPointerSidewaysScript : MonoBehaviour {

    
    private bool movingLeft;
    private GameObject wizzard;


	void Start () {
        wizzard = GameObject.Find("wizzard");
        movingLeft = true;
    }

	void Update () {
        if (wizzard.GetComponent<MagicController>().wizzardState == WizzardState.sideways)
        {
            Vector3 Rotation = gameObject.transform.rotation.eulerAngles;
            if ( movingLeft) 
            {
                transform.Rotate(0, 0, -1.5f);
                if (Rotation.y < 320 && Rotation.y > 150)
                {
                    movingLeft = false;
                }
            }
            else if (!movingLeft) 
            {
                transform.Rotate(0, 0, 1.5f);
                if (Rotation.y > 50 && Rotation.y < 100)
                {
                    movingLeft = true;
                }
            }
        }
    }
}
