using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEState
{
    void Enter(EnemyController enemy);
    void Execute(EnemyController enemy);
    void Exit(EnemyController enemy);

}
