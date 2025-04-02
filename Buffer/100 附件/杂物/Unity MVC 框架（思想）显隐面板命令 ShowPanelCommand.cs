using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        //写面板创建的逻辑 
        string panelName = notification.Body.ToString();

        switch(panelName)
        {
            case "MainPanel":
                //显示面板相关内容

                //如果要使用Mediator 一定也要在Facade中去注册
                //command、proxy都是一样的 要用 就要注册
                //可以在命令 中 直接使用Facade代表的就是唯一的 Facade

                //判断如果没有mediator就去new一个 
                if( !Facade.HasMediator(NewMainViewMediator.NAME) )
                {
                    Facade.RegisterMediator(new NewMainViewMediator());
                }
                //有mediator了 下一步 就是去创建界面 创建预设体 

                //Facade 得到Mediator的方法
                NewMainViewMediator mm = Facade.RetrieveMediator(NewMainViewMediator.NAME) as NewMainViewMediator;
                //实例化面板对象
                if (mm.ViewComponent == null)
                {
                    GameObject res = Resources.Load<GameObject>("UI/MainPanel");
                    GameObject obj = GameObject.Instantiate(res);
                    //设置它的父对象 为Canvas
                    obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    //得到预设体上的 view脚本 关联到 mediator上
                    mm.SetView(obj.GetComponent<NewMainView>());
                }
                //往往现实了面板后 需要在这里进行第一次更新
                //需要把 数据一起通过参数 传出去
                SendNotification(PureNotification.UPDATE_PLAYER_INFO, Facade.RetrieveProxy(PlayerProxy.NAME).Data);

                break;
            case "RolePanel":
                //判断如果没有mediator就去new一个 
                if (!Facade.HasMediator(NewRoleViewMediator.NAME))
                {
                    Facade.RegisterMediator(new NewRoleViewMediator());
                }
                //有mediator了 下一步 就是去创建界面 创建预设体 

                //Facade 得到Mediator的方法
                NewRoleViewMediator rm = Facade.RetrieveMediator(NewRoleViewMediator.NAME) as NewRoleViewMediator;
                //实例化面板对象
                if (rm.ViewComponent == null)
                {
                    GameObject res = Resources.Load<GameObject>("UI/RolePanel");
                    GameObject obj = GameObject.Instantiate(res);
                    //设置它的父对象 为Canvas
                    obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    //得到预设体上的 view脚本 关联到 mediator上
                    rm.SetView(obj.GetComponent<NewRoleView>());
                }
                SendNotification(PureNotification.UPDATE_PLAYER_INFO, Facade.RetrieveProxy(PlayerProxy.NAME).Data);
                break;
        }
    }
}
