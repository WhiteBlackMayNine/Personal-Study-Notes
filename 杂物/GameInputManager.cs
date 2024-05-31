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
        _gameInputAction ??= new GameInputAction();//�ж��ǲ���Ϊ�յģ�����Ǿ�Newһ���µ�
    }

    private void OnEnable()
    {
        _gameInputAction.Enable();//�����ã���������ȥû�з�Ӧ
    }

    private void OnDisable()
    {
        _gameInputAction.Disable();
    }
}
