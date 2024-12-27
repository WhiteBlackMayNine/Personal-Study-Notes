using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Login.Panel
{
    public class RegisterPanel : BasePanel
    {
        //确定 取消 按钮
        public Button sureButton;
        public Button cancelButton;

        //账号 密码 文本输入组件
        public InputField usernameField;
        public InputField passwordField;
        
        
        
        public override void Init()
        {
            cancelButton.onClick.AddListener(() =>
            {
                //隐藏自己
                UIManager.Instance.HidePanel<RegisterPanel>();
                //显示登录面板
                UIManager.Instance.ShowPanel<LoginPanel>();
            });
            
            sureButton.onClick.AddListener(() =>
            {
                //判断输入的账号密码是否合理
                //(这里认为；账号密码必须大于 6 位)
                if (usernameField.text.Length <= 6 ||
                    passwordField.text.Length <= 6)
                {
                    //不合法 调用提示面板
                    var tipPanel =  UIManager.Instance.ShowPanel<TipPanel>();
                    //改变提示面板上的提示内容
                    tipPanel.ChangeTip("账号和密码必须大于6位");
                    return;
                }
                
                //去注册账号密码
                //因为 RegisterUser 返回值为 true 所以使用 if 进行判断
                if (LoginMgr.Instance.RegisterUser(usernameField.text, passwordField.text))
                {
                    //注册成功
                    //RegisterUser 方法中，已经进行了存储数据 所以这里不需要进行额外处理
                    
                    //显示登录面板
                    var loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                    //更新 登录面板上的 用户名 和 密码
                    loginPanel.SetInfo(usernameField.text, passwordField.text);
                    //隐藏自己
                    UIManager.Instance.HidePanel<RegisterPanel>();
                }
                else
                {
                    //注册失败
                    //提示 用户名已存在
                    var tipPanel =  UIManager.Instance.ShowPanel<TipPanel>();
                    tipPanel.ChangeTip("用户名已存在");
                    //方便二次输入，清空文本
                    usernameField.text = "";
                    passwordField.text = "";
                }
                
                
            });
        }
    }
}