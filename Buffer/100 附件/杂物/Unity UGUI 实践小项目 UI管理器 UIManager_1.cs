using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIManager
    {

        #region 单例模式

        private static UIManager _instance;

        public static UIManager Instance
        {
            get { return _instance ??= new UIManager(); }
        }

        private UIManager()
        {
            _canvasTransform = GameObject.Find("Canvas").transform;
            //让 Canvas 对象 过场景不移除
            //我们都是通过 动态创建 和 动态删除 来显示 隐藏 面板的
            //所有不删除 Canvas 影响不大(可以控制其隐藏，来打到消除界面的效果)
            Object.DontDestroyOnLoad(_canvasTransform.gameObject);
        }

        #endregion

        //存储面板的容器
        //里氏替换 面板都会继承 BasePanel 父类装子类
        private Dictionary<string,BasePanel> _panDic = new Dictionary<string, BasePanel>();
        
        //Canvas 对象 在构造函数中获取
        private Transform _canvasTransform;
        
        //显示面板
        public T ShowPanel<T>() where T : BasePanel
        {
            //显示面板 就是 动态的创建面板预制体 设置父对象(即，设置在 Canvas 下)
            //利用泛型，根据传入的类型来返回出不同的面板
            //我们只需要保证 泛型T的类型 和 面板名 一致，定一个这样的规则，就可以使用了
            var panelName = typeof(T).Name;

            if (_panDic.TryGetValue(panelName, out var value))
            {
                //检查面板是否已经显示出来了，如果显示出来就直接返回
                //不用再创建出来
                return value as T;
            }
            
            //根据得到的类名 就是我们预制体面板名
            //直接动态创建便可( 参数: 要创建的对象 父对象位置 是否为世界坐标)
            GameObject panelObj = Object.Instantiate(Resources.Load<GameObject>("UI/" + panelName),
                _canvasTransform, false);
            
            //得到 对应面板脚本的对象 存储起来
            var panel = panelObj.GetComponent<T>();
            //存储到对应容器中，之后方便我们获取它
            _panDic.Add(panelName, panel);
            //调用 显示自己(ShowMe) 方法
            panel.ShowMe();
            
            return panel;
        }
        
        //隐藏面板
        //参数一 如果希望淡出效果，就默认传入 true 如果希望直接隐藏(删除)面板，那么就传入 false
        public void HidePanel<T>(bool isFade = true) where T : BasePanel
        {
            var panelName = typeof(T).Name;

            if (!_panDic.ContainsKey(panelName)) return;
            
            if (isFade)
            {
                _panDic[panelName].HideMe(() =>
                {
                    //删除物体 并 从字典中移除
                    Object.Destroy(_panDic[panelName].gameObject);
                    _panDic.Remove(panelName);
                });
            }
            else
            {
                //删除物体 并 从字典中移除
                Object.Destroy(_panDic[panelName].gameObject);
                _panDic.Remove(panelName);
            }
        }
        
        //获得面板
        public T GetPanel<T>() where T : BasePanel
        {
            var panelName = typeof(T).Name;
            if (_panDic.ContainsKey(panelName))
            {
                return (T)_panDic[panelName];
            }
            else
            {
                return null;
            }
        }
    }
}