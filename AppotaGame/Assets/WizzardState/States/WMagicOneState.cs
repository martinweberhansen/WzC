using UnityEngine;
using System.Collections;
using System;

public class WMagicOneState : IWizzardState {

    private readonly WizzardStatePattern wzp;
    private MagicController magicCtrl;
    private Animator anim;
    int magicOneHash = Animator.StringToHash("MagicOne");

    public WMagicOneState(WizzardStatePattern wzp, Animator anim, MagicController magicCtrl)
    {
        this.anim = anim;
        this.wzp = wzp;
        this.magicCtrl = magicCtrl;
    }

  
    public void UpdateState()
    {
        if(magicCtrl.wizzardState == WizzardState.idle)
        {
            ToIdleState();
        }
        else if (magicCtrl.wizzardState == WizzardState.sideways && magicCtrl.selectedMagic == SelectedMagic.fireBall)
        {
            ToMagicOneState();
        }
        //else if (magicCtrl.wizzardState == WizzardState.sideways && magicCtrl.selectedMagic == SelectedMagic.MagicWall)
        //{
        //    ToMagicTwoState();
        //}
        else
        {
            ToIdleState();
        }
    }

    public void ToMagicTwoState()
    {
        //has to move to idleState first.
        //wzp.currentState = wzp.MagicTwoState;
    }

    public void ToMagicOneState()
    {
        //cant transition to same state
    }

    public void ToIdleState()
    {
        anim.ResetTrigger(magicOneHash);
        wzp.currentState = wzp.IdleState;
    }

    public void ToDeathState()
    {
        throw new NotImplementedException();
    }

    public void PlayAnimation()
    {
        anim.SetTrigger(magicOneHash);
    }
}
