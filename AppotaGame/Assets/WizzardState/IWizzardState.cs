using UnityEngine;
using System.Collections;

public interface IWizzardState {

    void UpdateState();

    void ToMagicTwoState();

    void ToMagicOneState();

    void ToIdleState();

    void ToDeathState();

    void PlayAnimation();
}
