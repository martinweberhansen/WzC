using UnityEngine;
using System.Collections;

public class DeathState : IUnitState {

    private Animator anim;
    private readonly UnitStatePattern sp;
    private UnitAI AI;
    int deadHash = Animator.StringToHash("Dead");

    public DeathState(UnitStatePattern UnitStatePattern, Animator anim)
    {
        sp = UnitStatePattern;
        this.anim = anim;
    }


    public void UpdateState()
    {
        PlayAnimation();
    }

    //public void InitializeState() { }

    public void ToWalkState()
    {

    }

    public void ToAttackState()
    {

    }

    public void ToIdleState()
    {

    }

    public void ToDeathState()
    {

    }

    public void PlayAnimation()
    {
        anim.SetTrigger(deadHash);
    }
}
