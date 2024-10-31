using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_CameraControl : MonoBehaviour
{

    #region 变量相关

    [SerializeField, Header("相机参数")] private float _cameraSpeed;
    [SerializeField] private Vector2 _cameraVerticalMaxAnagle;
    [SerializeField] private float _cameraSmoothSpeed;//设置相机平滑旋转的时间
    private Transform _lookTarget;
    [SerializeField] private float _positionOffset;//设置相机的偏移量，控制相机与目标的距离
    [SerializeField] private float _positionSmoothTime;//设置相机平滑移动的时间

    private Vector2 _input;//用来接受鼠标输入的值
    private Vector3 _smoothDampVelocity = Vector3.zero;
    private Vector3 _cameraRotationAngle;//相机旋转的角度

    private Transform _currentLookTarget;//用于 处决时相机看着敌人
    private bool _isFinish;//用来判断当前是否处于处决状态

    #endregion

    #region 生命周期函数

    private void Awake()
    {
        _lookTarget = GameObject.FindWithTag("CameraLookTarget").transform;
        _currentLookTarget = _lookTarget;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraInput();
    }

    private void OnEnable()
    {
        GameEventManager.MainInstance.AddEventListening<Transform, float>("设置相机位置", SetFinishTarget);
    }

    private void OnDisable()
    {
        GameEventManager.MainInstance.RemoveEvent<Transform, float>("设置相机位置", SetFinishTarget);
    }

    private void LateUpdate()
    {
        UpdateCameraRotation();
        CameraPosition();
    }

    #endregion

    #region 函数相关

    #region 得到鼠标输入

    /// <summary>
    /// 得到鼠标输入
    /// </summary>
    private void CameraInput()
    {
        //intput.y 就是水平移动/左右移动 以Y轴为基准左右看
        _input.y += GameInputManager.MainInstance.CameraLook.x;

        //上下看，获取鼠标的Y轴，左右则是鼠标的X轴
        _input.x -= GameInputManager.MainInstance.CameraLook.y;
        //限制一下范围
        _input.x = Mathf.Clamp(_input.x, _cameraVerticalMaxAnagle.x, _cameraVerticalMaxAnagle.y);
    }

    #endregion

    #region 相机旋转

    /// <summary>
    /// 相机旋转
    /// </summary>
    private void UpdateCameraRotation()
    {
        _cameraRotationAngle = Vector3.SmoothDamp(_cameraRotationAngle, new Vector3(_input.x, _input.y, 0f),
            ref _smoothDampVelocity, _cameraSmoothSpeed);
        transform.eulerAngles = _cameraRotationAngle;
    }

    #endregion

    #region 相机位置移动

    /// <summary>
    /// 相机位置移动
    /// </summary>
    private void CameraPosition()
    {
        //以当前看着的目标为基准，往后偏移一段距离(由 _positionOffset 决定)
        //var newPosition = _lookTarget.position + (-transform.forward * _positionOffset);
        //加了处决看着敌人的功能用下面的 没加就用上面的
        var newPosition = ((((_isFinish) ? _currentLookTarget.position + _currentLookTarget.up * 0.7f :
        _currentLookTarget.position) + (-transform.forward * _positionOffset)));
        this.transform.position = Vector3.Lerp(this.transform.position, newPosition, _positionSmoothTime);
    }

    #endregion

    #region 处决时相机看着敌人

    private void SetFinishTarget(Transform target, float time)
    {
        _isFinish = true;
        _currentLookTarget = target;

        GameTimeManager.MainInstance.TryGetTimer(time, ResetTarget);
    }

    private void ResetTarget()
    {
        _isFinish = false;
        _currentLookTarget = _lookTarget;
    }

    #endregion

    #endregion
}
