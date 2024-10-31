using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_CameraControl : MonoBehaviour
{

    #region �������

    [SerializeField, Header("�������")] private float _cameraSpeed;
    [SerializeField] private Vector2 _cameraVerticalMaxAnagle;
    [SerializeField] private float _cameraSmoothSpeed;//�������ƽ����ת��ʱ��
    private Transform _lookTarget;
    [SerializeField] private float _positionOffset;//���������ƫ���������������Ŀ��ľ���
    [SerializeField] private float _positionSmoothTime;//�������ƽ���ƶ���ʱ��

    private Vector2 _input;//����������������ֵ
    private Vector3 _smoothDampVelocity = Vector3.zero;
    private Vector3 _cameraRotationAngle;//�����ת�ĽǶ�

    private Transform _currentLookTarget;//���� ����ʱ������ŵ���
    private bool _isFinish;//�����жϵ�ǰ�Ƿ��ڴ���״̬

    #endregion

    #region �������ں���

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
        GameEventManager.MainInstance.AddEventListening<Transform, float>("�������λ��", SetFinishTarget);
    }

    private void OnDisable()
    {
        GameEventManager.MainInstance.RemoveEvent<Transform, float>("�������λ��", SetFinishTarget);
    }

    private void LateUpdate()
    {
        UpdateCameraRotation();
        CameraPosition();
    }

    #endregion

    #region �������

    #region �õ��������

    /// <summary>
    /// �õ��������
    /// </summary>
    private void CameraInput()
    {
        //intput.y ����ˮƽ�ƶ�/�����ƶ� ��Y��Ϊ��׼���ҿ�
        _input.y += GameInputManager.MainInstance.CameraLook.x;

        //���¿�����ȡ����Y�ᣬ������������X��
        _input.x -= GameInputManager.MainInstance.CameraLook.y;
        //����һ�·�Χ
        _input.x = Mathf.Clamp(_input.x, _cameraVerticalMaxAnagle.x, _cameraVerticalMaxAnagle.y);
    }

    #endregion

    #region �����ת

    /// <summary>
    /// �����ת
    /// </summary>
    private void UpdateCameraRotation()
    {
        _cameraRotationAngle = Vector3.SmoothDamp(_cameraRotationAngle, new Vector3(_input.x, _input.y, 0f),
            ref _smoothDampVelocity, _cameraSmoothSpeed);
        transform.eulerAngles = _cameraRotationAngle;
    }

    #endregion

    #region ���λ���ƶ�

    /// <summary>
    /// ���λ���ƶ�
    /// </summary>
    private void CameraPosition()
    {
        //�Ե�ǰ���ŵ�Ŀ��Ϊ��׼������ƫ��һ�ξ���(�� _positionOffset ����)
        //var newPosition = _lookTarget.position + (-transform.forward * _positionOffset);
        //���˴������ŵ��˵Ĺ���������� û�Ӿ��������
        var newPosition = ((((_isFinish) ? _currentLookTarget.position + _currentLookTarget.up * 0.7f :
        _currentLookTarget.position) + (-transform.forward * _positionOffset)));
        this.transform.position = Vector3.Lerp(this.transform.position, newPosition, _positionSmoothTime);
    }

    #endregion

    #region ����ʱ������ŵ���

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
