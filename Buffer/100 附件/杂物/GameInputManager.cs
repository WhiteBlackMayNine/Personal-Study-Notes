using UnityEngine;
using UnityEngine.InputSystem;
using GGG.Tool.Singleton;

public class GameInputManager : Singleton<GameInputManager>
{
    private GameInputAction _gameInputAction;

    //移动，读取输入的一个 Vector2 的值
    public Vector2 Movement => _gameInputAction.Player.Movement.ReadValue<Vector2>();

    //摄像机移动同理
    public Vector2 CameraLook => _gameInputAction.Player.CameraLook.ReadValue<Vector2>();

    //检测按键输入  如果按下了这个键，那么返回值为 true
    public bool Run => _gameInputAction.Player.Run.triggered;

    public bool Walk => _gameInputAction.Player.Walk.triggered;

    //长按按键   一直按着的时候会返回 true
    public bool Parry => _gameInputAction.Player.Parry.phase == InputActionPhase.Performed;

    public bool LAttack => _gameInputAction.Player.LAttack.triggered;

    public bool RAttack => _gameInputAction.Player.RAttack.triggered;

    public bool Finish => _gameInputAction.Player.Finish.triggered;

    public bool TakeOut => _gameInputAction.Player.TakeOut.triggered;

    #region 生命周期函数

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
