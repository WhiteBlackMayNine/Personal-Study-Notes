using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GGG.Tool.Singleton;

public class GameInputManager : Singleton<GameInputManager>
{
    private GameInputAction _gameInputAction;

    public Vector2 Movemeng => _gameInputAction.GameInput.Movement.ReadValue<Vector2>();
    public Vector2 CameraLook => _gameInputAction.GameInput.CameraLook.ReadValue<Vector2>();



    protected override void Awake()
    {
        base.Awake();
        _gameInputAction ??= new GameInputAction();//判断是不是为空的，如果是就New一个新的
    }

    private void OnEnable()
    {
        _gameInputAction.Enable();//不启用，按键按下去没有反应
    }

    private void OnDisable()
    {
        _gameInputAction.Disable();
    }
}
