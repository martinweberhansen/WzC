using UnityEngine;
using System.Collections;

public interface IUnitState {

    void UpdateState();

    ////void InitializeState();

    void ToWalkState();

    void ToAttackState();

    void ToIdleState();

    void ToDeathState();

    void PlayAnimation();
}
