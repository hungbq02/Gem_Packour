using UnityEngine;

public class ClimbState : BaseState
{
    private float climbSpeed = 4f;
    bool isClimbing;

    bool isJumping;
    private MovementSM sm;
    public ClimbState(PlayerController playerController, MovementSM stateMachine) : base(playerController, stateMachine)
    {
        sm = (MovementSM)this.stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        gravityVelocity = Vector3.zero;

        playerController.Animator.SetFloat("InputMagnitude", 0f);
        playerController.Animator.SetBool("OnGround", false);
        playerController.Animator.ResetTrigger("Ground");
    }



    public override void HandleInput()
    {
        base.HandleInput();
        if (playerController.Input.jump)
        {
            isJumping = true;
        }
        velocity = new Vector3(0f, playerController.Input.move.y, 0f);
        playerController.Animator.SetFloat("ClimbY", velocity.magnitude);
    }

    public override void UpdateLogic()
    {
        playerController.Animator.SetTrigger("Climb");
        isClimbing = playerController.CanClimb();
        base.UpdateLogic();
        if (playerController.Controller.isGrounded || !isClimbing)
        {

            sm.ChangeState(sm.groundedState);
        }
        if (isJumping)
        {
            sm.ChangeState(sm.jumpingState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector3 move = velocity.normalized * climbSpeed * Time.deltaTime;
        playerController.Controller.Move(move);
    }
    public override void Exit()
    {
        base.Exit();
        isJumping = false;
        playerController.Animator.ResetTrigger("Climb");
    }
}
