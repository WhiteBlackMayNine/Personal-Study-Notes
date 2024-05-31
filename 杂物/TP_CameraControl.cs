using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_CameraControl : MonoBehaviour
{ 
    //������ƶ��ٶ�
    [SerializeField, Header("�����������")] private float _controlSpeed;
    [SerializeField] private Vector2 _cameraVerticalMaxAngle;//����������¿������Ƕ�
    [SerializeField] private float _smoothSpeed; //�������ƽ���ƶ���ʱ��
    private Transform _lookTarget;//��������Ŀ���Ŀ��
    [SerializeField] private float _positionOffset;//���������ƫ���������������Ŀ��ľ���
    [SerializeField] private float _positionSmoothTime;//����ƽ��ʱ��
    private Vector3 _smoothDampVelocity = Vector3.zero;
    private Vector2 _intput;//���������������ƶ���ֵ
    private Vector3 _cameraRotation;//���������������ת��ֵ


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

    //�õ�������ƶ�
    private void CameraInput()
    {
        _intput.y += GameInputManager.MainInstance.CameraLook.x * _controlSpeed; //intput.y ����ˮƽ�ƶ�/�����ƶ� ��Y��Ϊ��׼���ҿ�
        _intput.x -= GameInputManager.MainInstance.CameraLook.y * _controlSpeed; //���¿�����ȡ����Y�ᣬ������������X��
        _intput.x = Mathf.Clamp(_intput.x, _cameraVerticalMaxAngle.x, _cameraVerticalMaxAngle.y);
    }
    //�������ת
    private void UpdateCameraRotation()
    {
        _cameraRotation = Vector3.SmoothDamp(_cameraRotation,new Vector3(_intput.x, _intput.y, 0f),
           ref _smoothDampVelocity,_smoothSpeed);//�����Բ�����ƶ�
        transform.eulerAngles = _cameraRotation;
    }
    private void CameraPosition()
    {
        //�Ե�ǰ������ŵ�Ŀ���λ��Ϊ��׼�������ƶ���Щ����
        var newPosition = (_lookTarget.position + (-transform.forward * _positionOffset));
        transform.position = Vector3.Lerp(transform.position, newPosition, DevelopmentToos.UnTetheredLerp(_positionSmoothTime));
    }

}
