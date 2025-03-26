using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingState : BaseState
{
    MovementSM sm;
    float timePassed;
    float landingTime; //Delay from jumpend -> move
    public LandingState(PlayerController playerController, MovementSM stateMachine) : base(playerController, stateMachine)
    {
        sm = (MovementSM)this.stateMachine;
    }

    public override void Enter()
    {
        timePassed = 0f;

        landingTime = 0.01f;

    }

    public override void UpdateLogic()
    {
        if (timePassed > landingTime)
        {
            //trigger back move
            playerController.Animator.SetTrigger("Ground");
            stateMachine.ChangeState(sm.groundedState);

        }
        timePassed += Time.deltaTime;
    }
}


