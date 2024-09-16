using BehaviorDesigner.Runtime.Tasks;
using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIComboAction : Action
{
    //写的攻击动作要在这里面执行

    private EnemyCombatControl _enemyCombatControl;

    public override void OnAwake()
    {
        _enemyCombatControl = GetComponent<EnemyCombatControl>();
    }


    public override TaskStatus OnUpdate()
    {
        if (_enemyCombatControl.GetAttackCommand())
        {
            _enemyCombatControl.AIBaseAttackInput();
            return TaskStatus.Running;
        }

        return TaskStatus.Success;
    }
}
