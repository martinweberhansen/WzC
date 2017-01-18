using UnityEngine;
using System.Collections;
using System;

public class WDeathState : IWizzardState {

    private readonly WizzardStatePattern wzp;
    private Animator anim;
    private MagicController magicCtrl;
    int deathHash = Animator.StringToHash("Dead");

    public WDeathState(WizzardStatePattern wzp, Animator anim, MagicController magicCtrl)
    {
        this.anim = anim;
        this.wzp = wzp;
        this.magicCtrl = magicCtrl;
    }
    

    public void UpdateState()
    {
        throw new NotImplementedException();
    }

    public void ToMagicTwoState()
    {
        throw new NotImplementedException();
    }

    public void ToMagicOneState()
    {
        throw new NotImplementedException();
    }

    public void ToIdleState()
    {
        throw new NotImplementedException();
    }

    public void ToDeathState()
    {
        throw new NotImplementedException();
    }

    public void PlayAnimation()
    {
        throw new NotImplementedException();
    }
}
