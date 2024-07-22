using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraCollider : MonoBehaviour
{
    /*
     *设置一个最大值和一个最小值
     *Layer 障碍物的层级
     *防止穿墙
     */
    [SerializeField, Header("最大最小的偏移量")]
    private Vector2 _maxDistanceOffset;
    [SerializeField, Header("障碍物的层级"), Space(height: 10)]
    private LayerMask _whatIsWall;
    [SerializeField, Header("射线长度"), Space(height: 10)]
    private float _detectionDistance;
    [SerializeField, Header("碰撞移动平滑时间"), Space(height: 10)]
    private float _colliderSmoothTime;


    //开始的时候，保存一下起始点 和起始的偏移量
    private Vector3 _originPosition;
    private float _originOffsetDistance;
    private Transform _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main.transform;

    }




    private void Start()
    {
        //相机会根据TP_CameraControl中的偏移量为参照物 归一化 使得相机初始位置在后面而不是前面（Z需要在0以下）
        _originPosition = this.transform.localPosition.normalized;
        _originOffsetDistance = _maxDistanceOffset.y;
    }

    private void LateUpdate()
    {
        UpdateCameraCollider();
    }

    //碰撞
    private void UpdateCameraCollider()
    {
        //设置射线的方向
        //需要转换为世界坐标  因为前面获取的是本地坐标
        var detectionDistance = this.transform.TransformPoint(_originPosition * _detectionDistance);
        if(Physics.Linecast(this.transform.position, detectionDistance,out var hit, _whatIsWall,
            QueryTriggerInteraction.Ignore))
        {
            //如果打到障碍物了，那么就需要让相机往前动
            _originOffsetDistance = Mathf.Clamp(hit.distance * 0.8f, _maxDistanceOffset.x, _maxDistanceOffset.y);
        }
        else
        {
            _originOffsetDistance = _maxDistanceOffset.y;
        }
        _mainCamera.localPosition = Vector3.Lerp(_mainCamera.localPosition,_originPosition*(_originOffsetDistance - 0.1f)
            , DevelopmentToos.UnTetheredLerp(_colliderSmoothTime));

        //0.8 和 0.1 可以改变
    }

}
