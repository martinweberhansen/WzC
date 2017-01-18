using UnityEngine;
using System.Collections;
using System;

public class WIdleState : IWizzardState {

    private readonly WizzardStatePattern wzp;
    private Animator anim;
    private MagicController magicCtrl;
    

    public WIdleState(WizzardStatePattern wzp, Animator anim, MagicController magicCtrl)
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
        else if (magicCtrl.wizzardState == WizzardState.sideways && magicCtrl.selectedMagic == SelectedMagic.fireBall ||
                 magicCtrl.wizzardState == WizzardState.vertical && magicCtrl.selectedMagic == SelectedMagic.fireBall ||

                 magicCtrl.wizzardState == WizzardState.sideways && magicCtrl.selectedMagic == SelectedMagic.timeBomb ||
                 magicCtrl.wizzardState == WizzardState.vertical && magicCtrl.selectedMagic == SelectedMagic.timeBomb
                 )
        {
            ToMagicOneState();
        }
        else if (magicCtrl.wizzardState == WizzardState.sideways && magicCtrl.selectedMagic == SelectedMagic.MagicWall ||
                 magicCtrl.wizzardState == WizzardState.vertical && magicCtrl.selectedMagic == SelectedMagic.MagicWall ||

                 magicCtrl.wizzardState == WizzardState.sideways && magicCtrl.selectedMagic == SelectedMagic.iceBall   ||
                 magicCtrl.wizzardState == WizzardState.vertical && magicCtrl.selectedMagic == SelectedMagic.iceBall
                 )
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
        wzp.currentState = wzp.MagicTwoState;
    }

    public void ToMagicOneState()
    {
        wzp.currentState = wzp.MagicOneState;
    }

    public void ToIdleState()
    {
        //cant transition to same state.
    }

    public void ToDeathState()
    {
        throw new NotImplementedException();
    }

    public void PlayAnimation()
    {
        
    }
}
