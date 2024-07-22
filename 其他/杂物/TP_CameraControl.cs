using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_CameraControl : MonoBehaviour
{ 
    //相机的移动速度
    [SerializeField, Header("相机参数配置")] private float _controlSpeed;
    [SerializeField] private Vector2 _cameraVerticalMaxAngle;//限制相机上下看的最大角度
    [SerializeField] private float _smoothSpeed; //设置相机平滑移动的时间
    private Transform _lookTarget;//设置相机的看向目标
    [SerializeField] private float _positionOffset;//设置相机的偏移量，控制相机与目标的距离
    [SerializeField] private float _positionSmoothTime;//设置平滑时间
    private Vector3 _smoothDampVelocity = Vector3.zero;
    private Vector2 _intput;//用来接受相机鼠标移动的值
    private Vector3 _cameraRotation;//用来接受相机的旋转的值


    private void Awake()
    {
        _lookTarget = GameObject.FindWithTag("CameraTarget").transform;
    }

    private void Update()
    {
        CameraInput();
    }

    private void LateUpdate()
    {
        UpdateCameraRotation();
        CameraPosition();    
    }

    //得到相机的移动
    private void CameraInput()
    {
        _intput.y += GameInputManager.MainInstance.CameraLook.x * _controlSpeed; //intput.y 就是水平移动/左右移动 以Y轴为基准左右看
        _intput.x -= GameInputManager.MainInstance.CameraLook.y * _controlSpeed; //上下看，获取鼠标的Y轴，左右则是鼠标的X轴
        _intput.x = Mathf.Clamp(_intput.x, _cameraVerticalMaxAngle.x, _cameraVerticalMaxAngle.y);
    }
    //相机的旋转
    private void UpdateCameraRotation()
    {
        _cameraRotation = Vector3.SmoothDamp(_cameraRotation,new Vector3(_intput.x, _intput.y, 0f),
           ref _smoothDampVelocity,_smoothSpeed);//让相机圆滑的移动
        transform.eulerAngles = _cameraRotation;
    }
    private void CameraPosition()
    {
        //以当前相机看着的目标的位置为基准，往后移动这些距离
        var newPosition = (_lookTarget.position + (-transform.forward * _positionOffset));
        transform.position = Vector3.Lerp(transform.position, newPosition, DevelopmentToos.UnTetheredLerp(_positionSmoothTime));
    }

}
