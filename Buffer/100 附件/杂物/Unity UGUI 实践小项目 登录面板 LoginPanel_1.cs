using Login.Data;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Login.Panel
{
    public class LoginPanel : BasePanel
    {
        //登录按钮 与 注册按钮
        public Button loginButton;
        public Button registerButton;
        
        //账号密码 文本输入控件
        public InputField passwordInput;
        public InputField usernameInput;
        
        //记住密码 自动登录 多选框
        public Toggle togPw;
        public Toggle togAuto;
        public override void Init()
        {
            //点击注册 做什么
            registerButton.onClick.AddListener(() =>
            {
                //TODO: 显示注册面板
                
                //隐藏自己
                UIManager.Instance.HidePanel<LoginPanel>();
            });
            //点击登录 做什么
            loginButton.onClick.AddListener(() =>
            {
                //TODO: 验证用户名、密码是否正确
            });
            
            //点击记住密码 逻辑
            togPw.onValueChanged.AddListener((isOn) =>
            {
                //如果记住密码没有被选中，那么自动登录也应该取消选中
                if (!isOn)
                {
                    togAuto.isOn = false;
                }
            });
            
            //点击自动登录 逻辑
            
            togAuto.onValueChanged.AddListener((isOn) =>
            {
                //选中自动登录时，如果记住密码没有被选中时，应该让它也选中
                if (isOn)
                {
                    togPw.isOn = true;
                }
            });
        }

        public override void ShowMe()
        {
            base.ShowMe();
            //显示自己时 根据数据 更新面板上的内容
            //得到数据
            var loginData = LoginMgr.Instance.LoginData;
            
            //初始化面板显示
            togPw.isOn = loginData.RememberMe;
            togAuto.isOn = loginData.AutoLogin;
            
            //更新账号密码
            usernameInput.text = loginData.Username;
            if (togPw.isOn)
            {
                //根据上一次是否勾选 记住密码 ，来决定是否更新密码
                passwordInput.text = loginData.Password;
            }

            if (togAuto.isOn)
            {
                //去验证账号密码，自动登录的逻辑
            }
            
        }
    }
}