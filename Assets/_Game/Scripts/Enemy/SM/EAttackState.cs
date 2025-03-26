using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAttackState : IEState
{
    public void Enter(EnemyController enemy)
    {
        enemy.StopMoving();
        enemy.Attack();
    }

    public void Execute(EnemyController enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.Target.transform.position);
        if (distance > 2.0f)
        {
            enemy.ChangeState(new EChaseState());
        }
    }

    public void Exit(EnemyController enemy)
    {
    }
}
