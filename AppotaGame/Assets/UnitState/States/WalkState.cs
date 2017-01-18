using UnityEngine;
using System.Collections;

public class WalkState : IUnitState {

    private Animator anim;
    private readonly UnitStatePattern unit;
    
    int walkHash = Animator.StringToHash("Walk");
    int runHash = Animator.StringToHash("Run");

    public WalkState(UnitStatePattern unitStatePattern, Animator anim)
    {
        unit = unitStatePattern;
        this.anim = anim;
    }


    public void UpdateState()
    {
        if(unit.IsDead())
        {
            ToDeathState();  
        }
        else if (unit.IsSetToIdle())
        {
            ToIdleState();
        }
        else if (unit.CarryingFlag())//hvis in motion
        {
            ToWalkState();
        }
        else if (unit.InRangeToAttack())
        {
            ToAttackState();
        }
        else if (unit.GetCurrentMovingSpeed() > 0.02f)
        {
            ToWalkState();
        }
        else
            ToIdleState();
    }

    //public void InitializeState() { }

    public void ToWalkState()
    {
    }

    public void ToAttackState()
    {
        anim.ResetTrigger(walkHash);
        anim.ResetTrigger(runHash);
        unit.currentState = unit.AttackState;
    }

    public void ToIdleState()
    {
        anim.ResetTrigger(walkHash);
        anim.ResetTrigger(runHash);
        unit.currentState = unit.IdleState;
    }

    public void ToDeathState()
    {
        anim.ResetTrigger(walkHash);
        anim.ResetTrigger(runHash);
        unit.currentState = unit.DeathState;
       // unit.Kill();
    }

    public void PlayAnimation()
    {
        if(unit.GetComponent<UnitStats>().speed >= 1)
        {
            anim.SetTrigger(runHash);
        }
        else
        {
            anim.SetTrigger(walkHash);
        }
        
    }
}
