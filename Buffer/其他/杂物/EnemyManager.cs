using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool.Singleton;
using Movement;
using Combat;
using BehaviorDesigner.Runtime;
using System;

public class EnemyManager : Singleton<EnemyManager>
{
    #region 变量相关

    private Transform _mainPlayer;//获取玩家位置信息

    /// <summary>
    /// 获取玩家位置信息
    /// </summary>
    /// <returns></returns>
    public Transform GetMainPlayer() => _mainPlayer;


    //这个列表用来保存未被激活的敌人
    [SerializeField] private List<GameObject> _allEnemies = new List<GameObject>();

    //这个列表用来保存激活的敌人
    [SerializeField] private List<GameObject> _activeEnemies = new List<GameObject>();

    private bool _closeAttackCommandCoroutine;//这个变量用于在外部控制关闭协程

    private WaitForSeconds _waitTime;

    #endregion

    #region 生命周期函数 

    protected override void Awake()
    {
        base.Awake();
        _mainPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        _waitTime = new WaitForSeconds(6f);
    }

    private void Start()
    {
        InitActiveEnemy();
        if (_activeEnemies.Count > 0)
        {
            _closeAttackCommandCoroutine = false;
        }

        //协程会牵扯到Update的执行逻辑，或者内存的泄漏
        StartCoroutine(EnableEnemyUnitAttackCommand());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    #endregion

    #region 函数相关

    #region 添加 / 删除 敌人

    public void AddEnemyUnit(GameObject enemy)
    {
        if (!_allEnemies.Contains(enemy))
        {
            _allEnemies.Add(enemy);
        }
    }

    public void RemovedEnemyUnit(GameObject enemy)
    {
        if (_activeEnemies.Contains(enemy))
        {
            EnemyMovementControl enemyMovementControl;
            if (enemy.TryGetComponent(out enemyMovementControl))
            {
                enemyMovementControl.EnableCharacterController(false);
            }
            _activeEnemies.Remove(enemy);
        }
    }

    #endregion

    #region 初始化敌人

    private void InitActiveEnemy()
    {
        foreach (var e in _allEnemies)
        {
            if (e.activeSelf)
            {
                EnemyMovementControl enemyMovementControl;
                if (e.TryGetComponent(out enemyMovementControl))
                {
                    enemyMovementControl.EnableCharacterController(true);
                }
                _activeEnemies.Add(e);
            }
        }
    }

    #endregion

    #region 处决时敌人停止攻击

    //执行某些动作时让其他单位停止动作  
    //也可以通过在玩家上使用枚举来代表玩家的不同状态
    //利用枚举进行监测
    public void StopAllActiveUnit()
    {
        //让当前所有激活的单位 停止动作 或者 攻击动作
        foreach (var e in _activeEnemies)
        {
            if (e.TryGetComponent(out EnemyCombatControl enemyCombatControl))
            {
                enemyCombatControl.StopAllAction();
            }
        }
    }

    #endregion

    #region 协程

    IEnumerator EnableEnemyUnitAttackCommand()
    {
        if (_activeEnemies == null)
        {
            yield break;//关闭协程.
        }
        if (_activeEnemies.Count == 0)
        {
            yield break;//关闭协程
        }

        while (_activeEnemies.Count > 0)
        {
            if (_closeAttackCommandCoroutine)
            {
                yield break;//关闭协程
            }

            var index = Random.Range(0, _activeEnemies.Count);

            if (index < _activeEnemies.Count)
            {
                GameObject _temp = _activeEnemies[index];
                if (_temp.TryGetComponent(out EnemyCombatControl enemyCombatControl))
                {
                    enemyCombatControl.SetAttackCommand(true);
                }
            }
            yield return _waitTime;
        }

        yield break;//关闭协程
    }

    public void CloseAttackCommandCoroutine()
    {
        _closeAttackCommandCoroutine = true;
        StopCoroutine(EnableEnemyUnitAttackCommand());
    }

    #endregion

    #endregion

}
