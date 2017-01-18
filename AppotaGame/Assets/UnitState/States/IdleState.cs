using UnityEngine;
using System.Collections;

public class IdleState : IUnitState {

    private Animator anim;
    private readonly UnitStatePattern sp;

    public IdleState(UnitStatePattern unitStatePattern, Animator anim)
    {
        this.sp = unitStatePattern;
        this.anim = anim;
    }

    public void UpdateState()
    {
        sp.StopMoving();

        if (sp.IsDead())
        {
            ToDeathState();
            //sp.Kill();
        }
        else if (sp.IsSetToIdle())
        {
            ToIdleState();
        }
        else if (sp.InRangeToAttack())
        {
            ToAttackState();//enemy.AttackTarget();
        }
        else if (sp.GetCurrentMovingSpeed() > 0.03f)
        {
            ToWalkState();
        }
        else
            ToIdleState();
    }

    //public void InitializeState() { }

    public void ToWalkState()
    {
        sp.currentState = sp.WalkState;
    }

    public void ToAttackState()
    {
        sp.currentState = sp.AttackState;
    }

    public void ToIdleState()
    {
    }

    public void ToDeathState()
    {
        sp.currentState = sp.DeathState;
        //sp.Kill();
    }

    public void PlayAnimation()
    {
        //the Idle state plays automaticly
    }
}
