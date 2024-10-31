using BehaviorDesigner.Runtime.Tasks;
using Combat;
using GGG.Tool;
using Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFreeMovementAction : Action
{
    #region 注释

    //在敌人对象没有被玩家攻击 或者 没有去执行其他行为
    //就会一直运行这个节点 一直移动

    //这个脚本 AI在自由状态下的 统一的移动节点，专门管理AI的移动
    //在没有被指派攻击指令时， 处于闲置的自由移动
    //自由的 左右/前后移动
    //或者播放某些动画
    //在离玩家过近时，需要让 AI 后退

    //AI的启动 先检测攻击指令 
    //如果攻击指令没有被激活，那么判断与目标的距离，  过远，那么就朝着目标移动过去
    //当抵达到一定距离中，攻击指令依旧没有被激活
    //那么就让AI做出各种其他动画
    //当攻击指令被激活，那么退出节点

    #endregion

    #region 变量相关

    private EnemyMovementControl _enemyMovementControl;
    private EnemyCombatControl _enemyCombatControl;

    //与玩家的距离
    private float DistranceForTarget() => DevelopmentToos.DistanceForTarget(
        EnemyManager.MainInstance.GetMainPlayer(), _enemyMovementControl.transform);

    private int _actionIndex;//动作索引，不同的值代表不同的动作
    private int _lastActionIndex;//上一次执行的动作
    private float _actionTimer;//动作时间，这个动作要维持多久  可以是固定的也可以是随机的

    #endregion

    #region 函数相关

    public override void OnAwake()
    {
        _enemyMovementControl = this.GetComponent<EnemyMovementControl>();
        _enemyCombatControl = this.GetComponent<EnemyCombatControl>();
        _lastActionIndex = _actionIndex;
        _actionTimer = 1f;
    }

    public override TaskStatus OnUpdate()
    {
        //判断自身是否被指派攻击
        if (!_enemyCombatControl.GetAttackCommand())
        {
            //这里去做 这个节点 的行为逻辑
            //距离大于8 就往前（冲着玩家）走
            //距离小于8但大于3 随机移动
            //距离在3一下 往后移动
            if (DistranceForTarget() > 8f)
            {
                _enemyMovementControl.SetAnimatorMovementValue(0, 1);
            }
            else if (DistranceForTarget() < 8f - 0.1f && DistranceForTarget() > 3f + 0.1f)
            {
                //在一定值内，左右移动
                //0.1f 为一个偏差值
                FreeMovement();
                UpdateFreeActionTime();
            }
            else//距离太近时，往后移动
            {
                _enemyMovementControl.SetAnimatorMovementValue(0f, -1f);//往后移动
            }

            return TaskStatus.Running;
        }
        else
        {
            //这里去做 退出逻辑
            DevelopmentToos.WTF(this.gameObject.name + "已被指派攻击行为");
        }
        return TaskStatus.Success;
    }

    #region 随机动作执行

    private void FreeMovement()
    {
        switch (_actionIndex)
        {
            case 0:  //假设为 往左移动
                _enemyMovementControl.SetAnimatorMovementValue(-1f, 0f);
                break;
            case 1:  // 往右移动
                _enemyMovementControl.SetAnimatorMovementValue(1f, 0f);
                break;
            case 2:
                _enemyMovementControl.SetAnimatorMovementValue(0f, 0f);
                break;
            case 3:
                _enemyMovementControl.SetAnimatorMovementValue(-1f, 1f);
                break;
            case 4:
                _enemyMovementControl.SetAnimatorMovementValue(1f, 1f);
                break;
                //更多动作以此类推
        }
    }

    #endregion

    #region 更新随机动作持续时间
    private void UpdateFreeActionTime()
    {
        if (_actionTimer > 0f)
        { 
            //更新移动时间
            _actionTimer -= Time.deltaTime;
        }

        if (_actionTimer <= 0f)
        {
            //移动时间小于等于0时  更新动作索引
            UpdateActionIndex();
        }
    }

    #endregion

    #region 更新随机动作的动作索引

    private void UpdateActionIndex()
    {
        //保存一下
        _lastActionIndex = _actionIndex;

        //更新动作索引  注意 Random 左包含右不包含
        _actionIndex = Random.Range(0, 5);

        //随机时间
        _actionTimer = Random.Range(1f, 3f);

        //如果更新后的动作索引等于上一次的动作索引
        if (_actionIndex == _lastActionIndex)
        {
            //再随机一次
            _actionIndex = Random.Range(0, 5);
        }
        if (_actionIndex == 3 || _actionIndex == 4)
        {
            //对特殊动作的时间进行修改
            _actionTimer = 1.5f;
        }
    }

    #endregion


    #endregion

}
