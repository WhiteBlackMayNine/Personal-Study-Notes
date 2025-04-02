using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpCommand : SimpleCommand
{
    //1.继承Command相关的脚本
    //2.重写里面的执行函数
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        //当命令被执行时 就会调用该方法
        //启动命令中 往往是做一些初始化操作

        //没有这个数据代理 才注册 有了就别注册
        if( !Facade.HasProxy(PlayerProxy.NAME) )
        {
            Facade.RegisterProxy(new PlayerProxy());
        }
    }

}
