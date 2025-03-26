using UnityEngine;

public class JumpState : BaseState
{
    private MovementSM sm;

    bool isGrounded;

    float gravityValue;
    float jumpHeight;
    float playerSpeed;

    float windForce;

    Vector3 airVelocity;


    public JumpState(PlayerController playerController, MovementSM stateMachine) : base(playerController, stateMachine)
    {
        sm = (MovementSM)this.stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        isGrounded = false;
        gravityValue = playerController.gravityValue;
        jumpHeight = sm.jumpHeight;
        playerSpeed = sm.speed;
        gravityVelocity.y = 0.0f;

        playerController.Animator.SetFloat("InputMagnitude", 0.0f);
        playerController.Animator.SetTrigger("Jump");
        playerController.Animator.SetBool("OnGround", true);
        Jump();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (isGrounded)
        {
            sm.ChangeState(sm.landingState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (!isGrounded)
        {
            playerController.Animator.SetTrigger("Jump");
            playerController.Animator.SetBool("OnGround", false);

            velocity = sm.playerVelocity;
            airVelocity = new Vector3(playerController.Input.move.x, 0.0f, playerController.Input.move.y);

            velocity = velocity.x * playerController.cameraTransform.right + velocity.z * playerController.cameraTransform.forward.normalized;
            velocity.y = 0.0f;

            airVelocity = airVelocity.x * playerController.cameraTransform.right + airVelocity.z * playerController.cameraTransform.forward.normalized;
            airVelocity.y = 0.0f;


            playerController.Controller.Move(gravityVelocity * Time.deltaTime + (airVelocity * sm.airControl + velocity * (1 - sm.airControl)) * playerSpeed * Time.deltaTime + playerController.windForce * Time.deltaTime);
        }

        gravityVelocity.y += gravityValue * 2f * Time.deltaTime;
        //isGrounded = playerController.IsGrounded();
        isGrounded = playerController.Controller.isGrounded;
        //rotate player
        if (airVelocity.sqrMagnitude > 0)
        {
            playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, Quaternion.LookRotation(airVelocity), playerController.rotationDampTime);
        }
    }

    public override void Exit()
    {
        playerController.Input.jump = false;
        base.Exit();
    }

    private void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(sm.jumpHeight * -3f * gravityValue);
    }
}