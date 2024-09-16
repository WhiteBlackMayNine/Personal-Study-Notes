using UnityEngine;
using UnityEngine.InputSystem;
using GGG.Tool.Singleton;

public class GameInputManager : Singleton<GameInputManager>
{
    private GameInputAction _gameInputAction;

    //�ƶ�����ȡ�����һ�� Vector2 ��ֵ
    public Vector2 Movement => _gameInputAction.Player.Movement.ReadValue<Vector2>();

    //������ƶ�ͬ��
    public Vector2 CameraLook => _gameInputAction.Player.CameraLook.ReadValue<Vector2>();

    //��ⰴ������  ������������������ô����ֵΪ true
    public bool Run => _gameInputAction.Player.Run.triggered;

    public bool Walk => _gameInputAction.Player.Walk.triggered;

    //��������   һֱ���ŵ�ʱ��᷵�� true
    public bool Parry => _gameInputAction.Player.Parry.phase == InputActionPhase.Performed;

    public bool LAttack => _gameInputAction.Player.LAttack.triggered;

    public bool RAttack => _gameInputAction.Player.RAttack.triggered;

    public bool Finish => _gameInputAction.Player.Finish.triggered;

    public bool TakeOut => _gameInputAction.Player.TakeOut.triggered;

    #region �������ں���

    protected override void Awake()
    {
        base.Awake();
        _gameInputAction ??= new GameInputAction();
    }

    private void OnEnable()
    {
        _gameInputAction.Enable();
    }

    private void OnDisable()
    {
        _gameInputAction.Disable();
    }

    #endregion



}
