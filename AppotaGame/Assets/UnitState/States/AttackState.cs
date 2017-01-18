using UnityEngine;
using System.Collections;

public class AttackState : IUnitState {

    private Animator anim;
    private readonly UnitStatePattern sp;
    private  UnitAI unitAI;
    int attackHash = Animator.StringToHash("Attack");
    int shootHash = Animator.StringToHash("Shoot");
    

    public AttackState(UnitStatePattern unitStatePattern, Animator anim, UnitAI AI)
    {
        this.sp = unitStatePattern;
        this.anim = anim;
        this.unitAI = AI;
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
        else if (sp.CarryingFlag())
        {
            ToWalkState();
        }
        else if (sp.InRangeToAttack())
        {
            ToAttackState();
        }
        else
            ToIdleState();
    }

    public void ToWalkState()
    {
        anim.ResetTrigger(attackHash);
        sp.currentState = sp.WalkState;
    }

    public void ToAttackState()
    {
        //cant transition to same state.
    }

    public void ToIdleState()
    {
        anim.ResetTrigger(attackHash);
        sp.currentState = sp.IdleState;
    }

    public void ToDeathState()
    {
        anim.ResetTrigger(attackHash);
        sp.currentState = sp.DeathState;
        //sp.Kill();
    }
    
    public void PlayAnimation()
    {
        if (unitAI.IsAttackStyleMelee())
        {
            anim.SetTrigger(attackHash);
        }
        else
        {
            anim.SetTrigger(shootHash);
        }
    }
}
