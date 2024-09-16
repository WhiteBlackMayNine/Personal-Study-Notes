using BehaviorDesigner.Runtime.Tasks;
using Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;

public class AIJustMoveForward : Action
{
    //这个节点用来解决 AI 被指派攻击行为后 走向玩家

    private EnemyMovementControl _enemyMovementControl;

    public override void OnAwake()
    {
        _enemyMovementControl = GetComponent<EnemyMovementControl>();
    }

    public override TaskStatus OnUpdate()
    {
        if (DevelopmentToos.DistanceForTarget(EnemyManager.MainInstance.GetMainPlayer(),
            _enemyMovementControl.transform) > 1.5f)
        {
            _enemyMovementControl.SetApplyMovement(true);
            _enemyMovementControl.SetAnimatorMovementValue(0f, 1f);
            return TaskStatus.Running;
        }

        return TaskStatus.Success;
    }

}
