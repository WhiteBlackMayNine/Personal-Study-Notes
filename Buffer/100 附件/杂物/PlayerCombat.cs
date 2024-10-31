using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;
using Combat;
using System;

public class PlayerCombat : CharacterCombatBase
{
    #region 变量相关

    #region 敌人检测相关

    //敌人通过 EnemyManager 进行检测玩家

    private Vector3 _detectionDirection;//检测方向
    [SerializeField, Header("攻击检测")] private float _detectionRang;//范围
    [SerializeField] private float _detectionDistance;//长度
    [SerializeField] private LayerMask _enemyLayer;//敌人的层级
    private Collider[] units;

    private Transform _cameraGameobject;//获取相机位置 用于敌人检测(第一种)中

    #endregion

    #endregion

    #region 生命周期函数

    protected override void Awake()
    {
        base.Awake();

        //获取相机位置 用于敌人检测(第一种)中
        if (Camera.main != null)
        {
            _cameraGameobject = Camera.main.transform;
        }
    }

    protected override void Update()
    {
        base.Update();
        //使用了第二种范围检测，第一种球形检测相关函数就注释了
        //UpdateDetectionDirection();

        //方法一和方法二任选其一
        //GetOneEnemyUnitWayOne();
        GetOneEnemyUnitWayTwo();
        //ClearEnemyWayOne();
        ClearEnemyWayTwo();

        CharacterBaseAttackInput();
        //关于 为什么不在父类的 Update调用 是因为敌人EnemyCombat 调用CharacterBaseAttackInput 
        //在行为树节点去调用，不是在 Mono 中的 Update 中调用 也就不能写父类中的 Update 中
        //于是就只能写在子类的 Update 中

        //CheackEnemyIsDie();
        CharacterFinishAttackInput();
        CharacterAssassinationInput();
    }

    private void OnEnable()
    {
        GameEventManager.MainInstance.AddEventListening<bool>("激活处决", EnableFinishEventHandle);
        GameEventManager.MainInstance.AddEventListening<Transform>("敌人死亡", CheckRemoveEnemy);
    }

    private void OnDisable()
    {
        GameEventManager.MainInstance.RemoveEvent<bool>("激活处决", EnableFinishEventHandle);
        GameEventManager.MainInstance.RemoveEvent<Transform>("敌人死亡", CheckRemoveEnemy);
    }

    private void FixedUpdate()
    {
        //使用了第二种范围检测，第一种球形检测相关函数就注释了
        //DetectionTarget();

        //方法一和方法二任选其一
        //GetNearUnitWayOne();
        GetNearUnitWayTwo();
    }

    #endregion

    #region 函数相关

    #region 角色攻击

    #region 是否允许攻击输入
    private bool CanBaseAttackInput()
    {
        if (!_canAttackInput)
        {
            return false;
        }

        if (_animator.AnimationAtTag("Parry"))
        {
            return false;
        }

        if (_animator.AnimationAtTag("Hit"))
        {
            return false;
        }

        return true;
    }
    #endregion

    #region 普通攻击输入

    protected override void CharacterBaseAttackInput()
    {
        base.CharacterBaseAttackInput();

        if (!CanBaseAttackInput())
        {
            return;
        }

        if (GameInputManager.MainInstance.LAttack)
        {
            if (_currentCombo == null || _currentCombo != _baseCombo)
            {
                ChangeComboData(_baseCombo);
            }
            ExecuteComboAction();
        }
        else if (GameInputManager.MainInstance.RAttack)
        {
            if (_currentComboCount >= 2)
            {
                ChangeComboData(_changeCombo);
                switch (_currentComboCount)
                {
                    case 2:
                        _currentComboIndex = 0;
                        break;
                    case 3:
                        _currentComboIndex = 1;
                        break;
                    case 4:
                        _currentComboIndex = 2;
                        break;
                    case 5:
                        _currentComboIndex = 3;
                        break;
                }
            }
            else
            {
                return;
            }
            ExecuteComboAction();
            _currentComboCount = 0;
        }
    }

    #endregion

    #region 位置同步

    protected override void MatchPosition()
    {
        base.MatchPosition();

        if (_animator.AnimationAtTag("Finish"))
        {
            //因为处决是在敌人的前方进行的 所以 看向敌人的后方
            transform.rotation = Quaternion.LookRotation(-_currentEnemy.forward);
            RunningMatch(_finishCombo, _FinishComboIndex);
        }
        else if (_animator.AnimationAtTag("Assassination"))
        {
            //因为暗杀是在敌人的后方进行的 所以 看向敌人的前方
            transform.rotation = Quaternion.LookRotation(_currentEnemy.forward);
            RunningMatch(_assassinationCombo, _FinishComboIndex);
        }
    }

    #endregion

    #endregion

    #region 敌人检测

    #region 第一种检测 球形检测

    //获取一定范围内的所有敌人 取一个最近的敌人作为目标

    private void UpdateDetectionDirection()//更新球形检测方向
    {
        _detectionDirection = (_cameraGameobject.forward * GameInputManager.MainInstance.Movement.y) +
            (_cameraGameobject.right * GameInputManager.MainInstance.Movement.x);

        //y轴的值归零防止浮在半空中
        _detectionDirection.Set(_detectionDirection.x, 0f, _detectionDirection.z);

        _detectionDirection = _detectionDirection.normalized;
    }

    private void DetectionTarget()
    {
        //判断检测一下有没有检测到敌人   敌人的图层是第九层  这里的 1<<9 就是检测敌人层级
        if (Physics.SphereCast(transform.position + (transform.up * 0.7f), _detectionRang, _detectionDirection,
            out var hit, _detectionDistance, 1 << 9, QueryTriggerInteraction.Ignore))
        {
            //进入 if 内部，说明已经检测到了敌人
            _currentEnemy = hit.collider.transform;
        }
    }

    #endregion

    #region 第二种检测 范围检测

    //以玩家为中心 取自定义的一个半径圆的范围内 获取其中的敌人
    //在当前玩家没有目标时 取距离玩家最近的敌人为目标
    //(1)方法一 ：获取一定范围内的所有敌人，选择一个距离最近的作为目前敌人
    //(2)方法二 ：找一个距离最近的目标 在距离一定范围内时不会改变目标(动态更新)

    #region 方法一
    //获取一定范围内的所有敌人，选择一个距离最近的作为目前敌人
    //当移动时，不会获取目标
    //使用时 建议把半径设置大一点 

    //得到附近的所有敌人
    private void GetNearUnitWayOne()
    {
        //如果当前的 目前敌人 为不空 代表已经有敌人了
        if (_currentEnemy != null)
        {
            return;
        }
        if (_animator.GetFloat(AnimationID.MovementID) > 0.7f)//移动时不去获取
        {
            return;
        }
        //获取一定范围内的敌人
        units = Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRang,
            _enemyLayer, QueryTriggerInteraction.Ignore);
    }

    //选择距离最近的敌人
    private void GetOneEnemyUnitWayOne()
    {
        //如果敌人数组为空 也就是长度为0 则返回
        if (units.Length == 0)
        {
            return;
        }

        //如果当前的 目前敌人 为不空 代表已经有敌人了 
        if (_currentEnemy != null)
        {
            return;
        }

        if (_animator.GetFloat(AnimationID.MovementID) > 0.7f)//移动时不去获取
        {
            return;
        }

        if (!_animator.AnimationAtTag("Attack"))
        {
            return;
        }

        //-----------------------------------------------------------------------

        Transform _temp_Enemy = null;
        var _distance = Mathf.Infinity;//Mathf.Infinity 表示正无穷大

        foreach (var e in units)
        {
            //依次去遍历附近的敌人并做比较，选取距离玩家最近的一个敌人作为当前目标
            var dis = DevelopmentToos.DistanceForTarget(e.transform, transform);
            if (dis < _distance)//冒泡排序进行比较
            {
                _temp_Enemy = e.transform;
                _distance = dis;
            }
        }

        //把最后遍历获取到距离玩家最近的一个敌人当作目标
        _currentEnemy = _temp_Enemy != null ? _temp_Enemy : _currentEnemy;

        //------------------------------------------------------------------------
    }

    //清空敌人
    private void ClearEnemyWayOne()
    {
        if (_currentEnemy == null)
        {
            return;
        }

        if (_animator.GetFloat(AnimationID.MovementID) > 0.7f)//移动时不去获取
        {
            _currentEnemy = null;
        }
    }

    #endregion

    #region 方法二
    //找一个距离最近的目标 在距离一定范围内时不会改变目标
    //随时更新


    private void GetNearUnitWayTwo()
    {
        //如果当前的 目前敌人 为不空 代表已经有敌人了 且 距离小于1.5 则返回 不更新目标
        if (_currentEnemy != null && DevelopmentToos.DistanceForTarget(_currentEnemy, transform) < 1.5f)
        {
            return;
        }

        //获取一定范围内的敌人
        units = Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRang,
            _enemyLayer, QueryTriggerInteraction.Ignore);
    }

    //选择距离最近的敌人
    private void GetOneEnemyUnitWayTwo()
    {
        //如果敌人数组为空 也就是长度为0 则返回
        if (units.Length == 0)
        {
            return;
        }

        //如果当前的 目前敌人 为不空 代表已经有敌人了 且 距离小于1.5 则返回 不更新目标
        if (_currentEnemy != null && DevelopmentToos.DistanceForTarget(_currentEnemy, transform) < 1.5f)
        {
            return;
        }

        //-----------------------------------------------------------------------

        Transform _temp_Enemy = null;
        var _distance = Mathf.Infinity;//Mathf.Infinity 表示正无穷大

        foreach (var e in units)
        {
            //依次去遍历附近的敌人并做比较，选取距离玩家最近的一个敌人作为当前目标
            var dis = DevelopmentToos.DistanceForTarget(e.transform, transform);
            if (dis < _distance)//冒泡排序进行比较
            {
                _temp_Enemy = e.transform;
                _distance = dis;
            }
        }

        //把最后遍历获取到距离玩家最近的一个敌人当作目标
        _currentEnemy = _temp_Enemy != null ? _temp_Enemy : _currentEnemy;

        //------------------------------------------------------------------------
    }

    //清空敌人
    private void ClearEnemyWayTwo()
    {
        if (_currentEnemy == null)
        {
            return;
        }

        if (_animator.GetFloat(AnimationID.MovementID) > 0.7f && DevelopmentToos.DistanceForTarget(_currentEnemy, transform) < 3f)
        {
            _currentEnemy = null;
        }
    }

    #endregion


    #endregion

    #endregion

    #region 绘图

    private void OnDrawGizmos()//绘图函数
    {
        Gizmos.DrawWireSphere(transform.position + (transform.up * 0.7f) +
            (_detectionDirection * _detectionDistance), _detectionRang);
    }
    #endregion

    #region 处决

    private bool CanSpecialAttack()//是否允许执行特殊攻击
    {
        if (_animator.AnimationAtTag("Finish"))
        {
            return false;
        }

        if (_animator.AnimationAtTag("Assassination"))
        {
            return false;
        }

        if (_currentEnemy == null)
        {
            return false;
        }

        if (!_canFinish)
        {
            return false;
        }


        if (_currentComboCount < 2) //当目前攻击次数小于2时，不可以处决
        {
            return false;
        }


        return true;
    }


    private void CharacterFinishAttackInput()
    {
        if (!CanSpecialAttack())
        {
            return;
        }

        if (GameInputManager.MainInstance.Finish)
        {
            //去播放对应的处决动画
            _FinishComboIndex = Random.Range(0, _finishCombo.TryGetComboMaxCount());
            _animator.Play(_finishCombo.TryGetOneComboAction(_FinishComboIndex));

            //呼叫事件中心，调用敌人注册的处决事件
            GameEventManager.MainInstance.CallEvent("触发处决", _finishCombo.TryGetOneHitAction(_FinishComboIndex,
                0), transform, _currentEnemy);

            ResetComboInfo();
            EnemyManager.MainInstance.StopAllActiveUnit();
            GameEventManager.MainInstance.CallEvent<Transform, float>("设置相机位置", _currentEnemy,
                _animator.GetCurrentAnimatorStateInfo(0).length - 1f);
            _currentComboCount = 0;
            _canFinish = false;
        }
    }

    private void EnableFinishEventHandle(bool apply)//激活处决
    {
        if (_canFinish)
        {
            return;
        }

        _canFinish = apply;
    }

    #endregion

    #region 暗杀

    /// <summary>
    /// 是否允许进行暗杀
    /// </summary>
    /// <returns></returns>
    private bool CanAssassination()
    {
        //当前没有目标
        if (_currentEnemy == null)
        {
            return false;
        }

        //距离
        if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 2f)
        {
            return false;
        }

        //角度
        if (Vector3.Angle(transform.forward, _currentEnemy.transform.forward) > 30f)
        {
            return false;
        }

        //当前在暗杀动画中
        if (_animator.AnimationAtTag("Assassination"))
        {
            return false;
        }

        //当前在处决动画中
        if (_animator.AnimationAtTag("Finish"))
        {
            return false;
        }

        return true;

    }

    private void CharacterAssassinationInput()
    {
        if (!CanAssassination())
        {
            return;//检测是否可以进行暗杀
        }

        if (GameInputManager.MainInstance.TakeOut)
        {
            //执行暗杀
            _FinishComboIndex = Random.Range(0, _assassinationCombo.TryGetComboMaxCount());//随机取一个索引值
            _animator.Play(_assassinationCombo.TryGetOneComboAction(_FinishComboIndex), 0, 0f);

            //呼叫敌人的被暗杀事件
            GameEventManager.MainInstance.CallEvent("触发处决", _assassinationCombo.TryGetOneHitAction(_FinishComboIndex,
               0), transform, _currentEnemy);//这个跟处决的事件是一样的
            ResetComboInfo();
        }
    }
    #endregion

    #region 目标死亡监听

    #region 方法一
    //CharacterHealthBase 中 当前目标死亡后移除当前目标 内容

    private void CheackEnemyIsDie()
    {
        if (_currentEnemy == null)
        {
            return;
        }

        if (_currentEnemy.TryGetComponent(out IHealth health))
        {
            if (_animator.AnimationAtTag("Motion") && health.OnDie() && !_animator.IsInTransition(0))
            {
                //.IsInTransition(0) 检查角色动画是否转换

                //如果玩家的动作执行完毕，同时敌人已经死亡，而且动画不在过渡中
                //如果动作还没有执行完毕，然后有某些动作可能需要去判断或依赖当前目标
                //那么我们在敌人死亡后直接移除当前目标，玩家就没办法获取到当前目标
                //就会报出 空引用
                _currentEnemy = null;
            }
        }
    }

    #endregion

    #region 方法二

    private void CheckRemoveEnemy(Transform enemy)
    {
        if (_currentEnemy == enemy)
        {
            //如果玩家的动作执行完毕了，同时，敌人已经死亡了
            //而且动画不在过渡中
            //如果动画还没有执行完毕，然后我们某些动作可能需要去判断或者依赖当前目标
            //那么我们在敌人死亡后直接就移除当前目标
            //那么玩家就没办法获取到当前目标
            //就会报空引用!
            _currentEnemy = null;
            _canFinish = false;
        }
    }

    #endregion

    #endregion

    #endregion
}
