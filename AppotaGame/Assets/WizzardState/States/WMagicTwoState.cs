using UnityEngine;
using System.Collections;
using System;

public class WMagicTwoState : IWizzardState {

    private readonly WizzardStatePattern wzp;
    private Animator anim;
    private MagicController magicCtrl;
    int magicTwoHash = Animator.StringToHash("MagicTwo");

    public WMagicTwoState(WizzardStatePattern wzp, Animator anim, MagicController magicCtrl)
    {
        this.anim = anim;
        this.wzp = wzp;
        this.magicCtrl = magicCtrl;
    }

    public void UpdateState()
    {
        if (magicCtrl.wizzardState == WizzardState.idle)
        {
            ToIdleState();
        }
        else if (magicCtrl.wizzardState == WizzardState.sideways && magicCtrl.selectedMagic == SelectedMagic.MagicWall)
        {
            ToMagicTwoState();
        }
        else
        {
            ToIdleState();
        }
    }

    public void ToMagicTwoState()
    {
        //cant transistion to same state
    }

    public void ToMagicOneState()
    {
        //has to move to idleState first
        //wzp.currentState = wzp.MagicOneState;
    }

    public void ToIdleState()
    {
        anim.ResetTrigger(magicTwoHash);
        wzp.currentState = wzp.IdleState;
    }

    public void ToDeathState()
    {
        anim.ResetTrigger(magicTwoHash);
        throw new NotImplementedException();
    }

    public void PlayAnimation()
    {
        anim.SetTrigger(magicTwoHash);
    }
}
