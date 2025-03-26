using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected PlayerController playerController;
    protected StateMachine stateMachine;

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    public BaseState(PlayerController playerController, MovementSM stateMachine)
    {
        this.playerController = playerController;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }
    public virtual void Exit()
    {
    }
    public virtual void HandleInput()
    {
    }
    public virtual void UpdateLogic()
    {
    }
    public virtual void UpdatePhysics()
    {
    }
}
