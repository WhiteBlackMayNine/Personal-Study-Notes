using BehaviorDesigner.Runtime.Tasks;
using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIComboAction : Action
{
    //д�Ĺ�������Ҫ��������ִ��

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
