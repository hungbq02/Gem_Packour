using UnityEngine;

public class EChaseState : IEState
{
    public void Enter(EnemyController enemy)
    {
    }

    public void Execute(EnemyController enemy)
    {
        if (enemy.Target == null)
            return;

        Vector3 direction = (enemy.Target.transform.position - enemy.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);

        enemy.Moving();

        float distance = Vector3.Distance(enemy.transform.position, enemy.Target.transform.position);
        if (distance <= 2.0f)
        {
            enemy.ChangeState(new EAttackState());
        }
        RaycastHit hit;
        Vector3 rayOrigin = enemy.transform.position + Vector3.up * 0.3f;
        Debug.DrawRay(rayOrigin, enemy.transform.forward * 2f, Color.red);
        if (Physics.Raycast(rayOrigin, enemy.transform.forward, out hit, 2f))
        {
            Debug.Log(hit.collider.name);
            if (!hit.collider.CompareTag("Player"))
            {
                enemy.ChangeState(new EJumpState());
                return;
            }
        }
    }

    public void Exit(EnemyController enemy)
    {
        enemy.StopMoving();
    }
}
