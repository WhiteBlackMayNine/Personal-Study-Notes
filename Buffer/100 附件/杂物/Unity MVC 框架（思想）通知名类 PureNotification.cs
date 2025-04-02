using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 这个是pureMVC中的 通知名类
/// 主要是用来申明各个通知的 名字 
/// 方便使用和管理
/// </summary>
public class PureNotification 
{
    /// <summary>
    /// 启动通知
    /// </summary>
    public const string START_UP = "startUp";
    /// <summary>
    /// 显示面板通知
    /// </summary>
    public const string SHOW_PANEL = "showPanel";
    /// <summary>
    /// 隐藏面板通知
    /// </summary>
    public const string HIDE_PANEL = "hidePanel";
    /// <summary>
    /// 代表玩家数据更新的通知名
    /// </summary>
    public const string UPDATE_PLAYER_INFO = "updatePlayerInfo";

    /// <summary>
    /// 升级通知
    /// </summary>
    public const string LEV_UP = "levUp";
}
