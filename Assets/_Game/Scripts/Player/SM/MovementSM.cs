using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class MovementSM : StateMachine
{

    [HideInInspector] public JumpState jumpingState;
    [HideInInspector] public LandingState landingState;
    [HideInInspector] public Grounded groundedState;
    [HideInInspector] public ClimbState climbState;



    [HideInInspector] protected PlayerController playerController;

    public float jumpHeight = 10f;
    public float speed = 5f;


    public float airControl = 1f; //control in air

    [HideInInspector] public Vector3 playerVelocity;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        jumpingState = new JumpState(playerController, this);
        groundedState = new Grounded(playerController, this);
        landingState = new LandingState(playerController, this);
        climbState = new ClimbState(playerController, this);
    }

    protected override BaseState GetInitialState()
    {
        return groundedState;
    }
}
