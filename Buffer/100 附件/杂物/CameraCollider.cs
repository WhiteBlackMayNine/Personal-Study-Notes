using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    #region ����

    [SerializeField, Header("ƫ����")]
    private Vector2 _maxDistanceOffset;//Vector2 ���Դ洢����ֵ

    [SerializeField, Header("�ϰ���㼶")]
    private LayerMask _whatIsWall;

    [SerializeField, Header("������")]
    private float _detectionDistance;

    [SerializeField, Header("��ײ�ƶ�ƽ��ʱ��")]
    private float _colliderSomoothTime;

    private Vector3 _originPosition;//��ʼλ��
    private float _originOffset;//��ʼ��ƫ����
    private Transform _mainCamera;//���������λ��

    #endregion

    #region �������ں���

    private void Awake()
    {
        _mainCamera = Camera.main.transform;
    }

    private void Start()
    {
        //��������TP_CameraControl�е�ƫ����Ϊ������ ��һ��
        //ʹ�������ʼλ���ں��������ǰ�棨Z��Ҫ��0���£�
        _originPosition = this.transform.localPosition.normalized;
        _originOffset = _maxDistanceOffset.y;
    }

    private void LateUpdate()
    {
        UpdateCameraCollider();
    }

    #endregion

    #region ���������ײ

    private void UpdateCameraCollider()
    {
        //��Ҫת��Ϊ��������  ��Ϊǰ���ȡ���Ǳ�������
        Vector3 _distanceDistance = this.transform.TransformPoint(_originPosition * _detectionDistance);
        //�������߼��
        if (Physics.Linecast(this.transform.position, _distanceDistance, out var hit, _whatIsWall,
            QueryTriggerInteraction.Ignore))
        {
            //_maxDistanceOffset �洢������ֵ��һ��Ϊ���ֵ(����Ϊy)��һ��Ϊ��Сֵ(����Ϊx)
            _originOffset = Mathf.Clamp(hit.distance * 0.8f, _maxDistanceOffset.x, _maxDistanceOffset.y);
        }
        else
        {
            _originOffset = _maxDistanceOffset.y;
        }
        if (!(_originOffset == _maxDistanceOffset.x))
        {
            _mainCamera.localPosition = Vector3.Lerp(_mainCamera.localPosition, _originPosition * (_originOffset - 0.1f),
                _colliderSomoothTime);
        }
    }

    #endregion 

}
