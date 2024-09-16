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
    #region �������

    private Transform _mainPlayer;//��ȡ���λ����Ϣ

    /// <summary>
    /// ��ȡ���λ����Ϣ
    /// </summary>
    /// <returns></returns>
    public Transform GetMainPlayer() => _mainPlayer;


    //����б���������δ������ĵ���
    [SerializeField] private List<GameObject> _allEnemies = new List<GameObject>();

    //����б��������漤��ĵ���
    [SerializeField] private List<GameObject> _activeEnemies = new List<GameObject>();

    private bool _closeAttackCommandCoroutine;//��������������ⲿ���ƹر�Э��

    private WaitForSeconds _waitTime;

    #endregion

    #region �������ں��� 

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

        //Э�̻�ǣ����Update��ִ���߼��������ڴ��й©
        StartCoroutine(EnableEnemyUnitAttackCommand());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    #endregion

    #region �������

    #region ��� / ɾ�� ����

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

    #region ��ʼ������

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

    #region ����ʱ����ֹͣ����

    //ִ��ĳЩ����ʱ��������λֹͣ����  
    //Ҳ����ͨ���������ʹ��ö����������ҵĲ�ͬ״̬
    //����ö�ٽ��м��
    public void StopAllActiveUnit()
    {
        //�õ�ǰ���м���ĵ�λ ֹͣ���� ���� ��������
        foreach (var e in _activeEnemies)
        {
            if (e.TryGetComponent(out EnemyCombatControl enemyCombatControl))
            {
                enemyCombatControl.StopAllAction();
            }
        }
    }

    #endregion

    #region Э��

    IEnumerator EnableEnemyUnitAttackCommand()
    {
        if (_activeEnemies == null)
        {
            yield break;//�ر�Э��.
        }
        if (_activeEnemies.Count == 0)
        {
            yield break;//�ر�Э��
        }

        while (_activeEnemies.Count > 0)
        {
            if (_closeAttackCommandCoroutine)
            {
                yield break;//�ر�Э��
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

        yield break;//�ر�Э��
    }

    public void CloseAttackCommandCoroutine()
    {
        _closeAttackCommandCoroutine = true;
        StopCoroutine(EnableEnemyUnitAttackCommand());
    }

    #endregion

    #endregion

}
