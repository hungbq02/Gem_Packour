using UnityEngine;

public class EJumpState : IEState
{
    private float jumpDuration = 1f;
    private float timer = 0f;

    private const float stuckDuration = 1.5f;

    public void Enter(EnemyController enemy)
    {
        //enemy.StopMoving();

        enemy.Anim.SetTrigger("Jump");
        Vector3 moveForce = enemy.transform.forward * 4f;
        Vector3 jumpForce = new Vector3(0, 8f, 0);
        enemy.Rb.AddForce(jumpForce + moveForce, ForceMode.Impulse);

        timer = 0f;
    }

    public void Execute(EnemyController enemy)
    {
        Debug.Log($"Gravity Scale: {Physics.gravity}");
        timer += Time.deltaTime;
        if (timer >= jumpDuration)
        {
            enemy.ChangeState(new EChaseState());
        }
        if (timer >= stuckDuration)
        {
            Debug.Log("Stuck");
            enemy.transform.Rotate(0, 90, 0);
            enemy.ChangeState(new EChaseState());
        }

    }

    public void Exit(EnemyController enemy)
    {
        enemy.Anim.ResetTrigger("Jump");
    }
}
