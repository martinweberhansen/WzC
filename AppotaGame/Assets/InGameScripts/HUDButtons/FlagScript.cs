using UnityEngine;
using System.Collections;

public class FlagScript : MonoBehaviour {

    bool isPickedUp;
    bool isTarget;
    public string flagName;
    public int flagNumber;
    
	void Start () {
        isTarget = false;
	}
    


    void Update () {
	    
	}

    public void SetIsPickedUp(bool ans)
    {
        isPickedUp = ans;
    }
    public bool IsPickedUp()
    {
        return isPickedUp;
    }
    public void SetIsTarget(bool ans)
    {
        isTarget = ans;
    }
    public bool IsTarget()
    {
        return isTarget;
    }

}
