using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    #region 变量

    [SerializeField, Header("偏移量")]
    private Vector2 _maxDistanceOffset;//Vector2 可以存储两个值

    [SerializeField, Header("障碍物层级")]
    private LayerMask _whatIsWall;

    [SerializeField, Header("检测距离")]
    private float _detectionDistance;

    [SerializeField, Header("碰撞移动平滑时间")]
    private float _colliderSomoothTime;

    private Vector3 _originPosition;//初始位置
    private float _originOffset;//初始的偏移量
    private Transform _mainCamera;//主摄像机的位置

    #endregion

    #region 生命周期函数

    private void Awake()
    {
        _mainCamera = Camera.main.transform;
    }

    private void Start()
    {
        //相机会根据TP_CameraControl中的偏移量为参照物 归一化
        //使得相机初始位置在后面而不是前面（Z需要在0以下）
        _originPosition = this.transform.localPosition.normalized;
        _originOffset = _maxDistanceOffset.y;
    }

    private void LateUpdate()
    {
        UpdateCameraCollider();
    }

    #endregion

    #region 更新相机碰撞

    private void UpdateCameraCollider()
    {
        //需要转换为世界坐标  因为前面获取的是本地坐标
        Vector3 _distanceDistance = this.transform.TransformPoint(_originPosition * _detectionDistance);
        //进行射线检测
        if (Physics.Linecast(this.transform.position, _distanceDistance, out var hit, _whatIsWall,
            QueryTriggerInteraction.Ignore))
        {
            //_maxDistanceOffset 存储的两个值，一个为最大值(这里为y)，一个为最小值(这里为x)
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
