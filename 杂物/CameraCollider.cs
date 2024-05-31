using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraCollider : MonoBehaviour
{
    /*
     *����һ�����ֵ��һ����Сֵ
     *Layer �ϰ���Ĳ㼶
     *��ֹ��ǽ
     */
    [SerializeField, Header("�����С��ƫ����")]
    private Vector2 _maxDistanceOffset;
    [SerializeField, Header("�ϰ���Ĳ㼶"), Space(height: 10)]
    private LayerMask _whatIsWall;
    [SerializeField, Header("���߳���"), Space(height: 10)]
    private float _detectionDistance;
    [SerializeField, Header("��ײ�ƶ�ƽ��ʱ��"), Space(height: 10)]
    private float _colliderSmoothTime;


    //��ʼ��ʱ�򣬱���һ����ʼ�� ����ʼ��ƫ����
    private Vector3 _originPosition;
    private float _originOffsetDistance;
    private Transform _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main.transform;

    }




    private void Start()
    {
        //��������TP_CameraControl�е�ƫ����Ϊ������ ��һ�� ʹ�������ʼλ���ں��������ǰ�棨Z��Ҫ��0���£�
        _originPosition = this.transform.localPosition.normalized;
        _originOffsetDistance = _maxDistanceOffset.y;
    }

    private void LateUpdate()
    {
        UpdateCameraCollider();
    }

    //��ײ
    private void UpdateCameraCollider()
    {
        //�������ߵķ���
        //��Ҫת��Ϊ��������  ��Ϊǰ���ȡ���Ǳ�������
        var detectionDistance = this.transform.TransformPoint(_originPosition * _detectionDistance);
        if(Physics.Linecast(this.transform.position, detectionDistance,out var hit, _whatIsWall,
            QueryTriggerInteraction.Ignore))
        {
            //������ϰ����ˣ���ô����Ҫ�������ǰ��
            _originOffsetDistance = Mathf.Clamp(hit.distance * 0.8f, _maxDistanceOffset.x, _maxDistanceOffset.y);
        }
        else
        {
            _originOffsetDistance = _maxDistanceOffset.y;
        }
        _mainCamera.localPosition = Vector3.Lerp(_mainCamera.localPosition,_originPosition*(_originOffsetDistance - 0.1f)
            , DevelopmentToos.UnTetheredLerp(_colliderSmoothTime));

        //0.8 �� 0.1 ���Ըı�
    }

}
