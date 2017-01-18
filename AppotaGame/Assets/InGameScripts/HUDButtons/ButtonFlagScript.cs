using UnityEngine;
using System.Collections;

public class ButtonFlagScript : MonoBehaviour {

	public void SetFlagAsTarget(int num)
    {
        GameAI.getGameAISingelton.SetTargetFlag(num);
    }
}
