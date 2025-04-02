using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : Facade
{
    //1.继承PureMVC中Facade脚本

    //2.为了方便我们使用Facade 需要自己写一个单例模式的属性
    public static GameFacade Instance
    {
        get
        {
            if( instance == null )
            {
                instance = new GameFacade();//Facade 继承 IFacade 接口，但为了方便使用，建议使用 as 转换一下
            }
            return instance as GameFacade;
        }
    }

    /// <summary>
    /// 3.初始化 控制层相关的内容
    /// </summary>
    protected override void InitializeController()
    {
        base.InitializeController();
        //这里面要写一些 关于 命令和通知 绑定的逻辑
        //参数一 传入通知名 参数二 传入一个函数，返回值为一个命令
        RegisterCommand(PureNotification.START_UP, () =>
        {
            return new StartUpCommand();//一个继承了 PureMVC 的命令
        });

        RegisterCommand(PureNotification.SHOW_PANEL, () =>
        {
            return new ShowPanelCommand();
        });

        RegisterCommand(PureNotification.HIDE_PANEL, () =>
        {
            return new HidePanelCommand();
        });

        RegisterCommand(PureNotification.LEV_UP, () =>
        {
            return new LevUpCommand();
        });
    }

    //4.一定是有一个启动函数的
    public void StartUp()
    {
        //发送通知
        SendNotification(PureNotification.START_UP);

        //SendNotification(PureNotification.SHOW_PANEL, "MainPanel");
    }
}
