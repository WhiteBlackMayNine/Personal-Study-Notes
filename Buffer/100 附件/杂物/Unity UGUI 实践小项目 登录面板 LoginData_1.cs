using UnityEngine;

namespace Login.Data
{
    //登录界面可能需要记住的玩家操作相关数据
    public class LoginData
    {
        #region 客户端相关
        
        //账号 密码
        public string Username;
        public string Password;
        
        //是否 记住密码 自动登录
        public bool RememberMe;
        public bool AutoLogin;

        #endregion

        #region 服务器相关
        
        //TODO:服务器相关数据        
        
        #endregion
    }
}