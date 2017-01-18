using UnityEngine;
using System.Collections;

public class WizzardStatePattern : MonoBehaviour {

    public Animator anim;


    [HideInInspector]
    public IWizzardState currentState;
    [HideInInspector]
    public WIdleState IdleState;
    [HideInInspector]
    public WMagicOneState MagicOneState;
    [HideInInspector]
    public WMagicTwoState MagicTwoState;
    [HideInInspector]
    public WDeathState DeathState;

    private MagicController magicCtrl;
    

    void Start () {
        anim = gameObject.GetComponent<Animator>();
        anim.speed = 2;
        magicCtrl = gameObject.GetComponent<MagicController>();

        IdleState       = new WIdleState(this, anim, magicCtrl);
        MagicOneState   = new WMagicOneState(this, anim, magicCtrl);
        MagicTwoState   = new WMagicTwoState(this, anim, magicCtrl);
        DeathState      = new WDeathState(this, anim, magicCtrl);

        currentState = IdleState;
    }
	
	void Update () {
        currentState.UpdateState();
        currentState.PlayAnimation();
    }

    public void StopWizzardAnim()
    {
        if(magicCtrl.wizzardState != WizzardState.idle)
            anim.speed = 0;
    }

    public void ResumeWizzardAnim()
    {
        anim.speed = 2;
    }
}
