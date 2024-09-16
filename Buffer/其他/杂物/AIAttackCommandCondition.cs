using BehaviorDesigner.Runtime.Tasks;
using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackCommandCondition : Conditional
{
    private EnemyCombatControl _enemyCombatControl;
    public override void OnAwake()
    {
        _enemyCombatControl = GetComponent<EnemyCombatControl>();
    }

    public override TaskStatus OnUpdate()
    {
        return (_enemyCombatControl.GetAttackCommand() ? TaskStatus.Success : TaskStatus.Failure);
    }

}
