using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMainViewMediator : Mediator
{
    public static new  string NAME = "NewMainViewMediator";
    //套路写法
    //1.继承PureMVC中的Mediator脚本 
    //2.写构造函数
    public NewMainViewMediator():base(NAME)
    {
        //这里面可以去创建界面预设体等等的逻辑
        //但是界面显示应该是触发的控制的
        //而且创建界面的代码 重复性比较高
    }

    public void SetView(NewMainView view)
    {
        ViewComponent = view;
        view.btnRole.onClick.AddListener(()=>
        {
            SendNotification(PureNotification.SHOW_PANEL, "RolePanel");
        });
    }

    //3.重写监听通知的方法
    public override string[] ListNotificationInterests()
    {
        //这是一个PureMVC的规则
        //就是你需要监听哪些通知 那就在这里把通知们通过字符串数组的形式返回出去
        //PureMVC就会帮助我们监听这些通知 
        // 类似于 通过事件名 注册事件监听
        return new string[]{
            PureNotification.UPDATE_PLAYER_INFO,
        };
    }

    //4.重写处理通知的方法
    public override void HandleNotification(INotification notification)
    {
        //INotification 对象 里面包含两个队我们来说 重要的参数
        //1.通知名 我们根据这个名字 来做对应的处理
        //2.通知包含的信息 
        switch (notification.Name)
        {
            case PureNotification.UPDATE_PLAYER_INFO:
                //收到 更新通知的时候 做处理
                if(ViewComponent != null)
                {
                    (ViewComponent as NewMainView).UpdateInfo(notification.Body as PlayerDataObj);
                }
                break;
        }
    }

    //5.可选：重写注册时的方法
    public override void OnRegister()
    {
        base.OnRegister();
        //初始化一些内容
    }
}
