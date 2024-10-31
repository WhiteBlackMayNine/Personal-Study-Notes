using BehaviorDesigner.Runtime.Tasks;
using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;

public class AIAttackDistanceCondition : Conditional
{
    //����ڵ����жϾ���


    private EnemyCombatControl _enemyCombatControl;
    [SerializeField] private float _attackDistance;
    public override void OnAwake()
    {
        _enemyCombatControl = GetComponent<EnemyCombatControl>();
    }

    public override TaskStatus OnUpdate()
    {
        return (DevelopmentToos.DistanceForTarget(EnemyManager.MainInstance.GetMainPlayer(),
            _enemyCombatControl.transform) < _attackDistance ? TaskStatus.Success : TaskStatus.Failure);
    }

}
