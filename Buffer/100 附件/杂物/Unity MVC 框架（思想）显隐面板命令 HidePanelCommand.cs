using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        //隐藏的目的
        //得到 mediator 再得到 mediator中的 view 然后去要不删除 要不 设置显隐
        //得到传入的 mediator
        Mediator m = notification.Body as Mediator;

        if( m != null && m.ViewComponent != null )
        {
            //直接删除场景上的面板对象
            GameObject.Destroy((m.ViewComponent as MonoBehaviour).gameObject);
            //删了后 一定要置空
            m.ViewComponent = null;
        }
    }
}
