using Login.Data;
using UnityEngine;

namespace Login
{
    public class LoginMgr
    {
        private static LoginMgr _instance;
        public static LoginMgr Instance => _instance ??= new LoginMgr();
        private LoginMgr()
        {
            //直接通过JsonMgr 管理器 来读取对应数据
            _loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
            _registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");
        }
        
        //为了直接管理数据
        //避免外部进行修改，不让外部去 new
        private LoginData _loginData;
        public LoginData LoginData => _loginData ??= new LoginData();
        
        //注册-数据
        private RegisterData _registerData;
        public RegisterData RegisterData => _registerData;

        #region 登录数据

        /// <summary>
        /// 存储登录相关数据
        /// </summary>
        public void SaveLoginData()
        {
            JsonMgr.Instance.SaveData(_loginData, "LoginData");
        }

        #endregion

        #region 注册数据

        //存储注册数据

        public void SaveRegisterData()
        {
            JsonMgr.Instance.SaveData(_registerData, "RegisterData");
        }
        
        //注册方法
        //返回值 ——> 当前是否注册成功
        
        public bool RegisterUser(string username, string password)
        {
            if (!_registerData.RegisterDataDic.TryAdd(username, password))
            {
                //如果字典中已经存在了 将要注册用户名
                //判断是否已经存在用户
                return false;
            }
            
            //如果不存在 证明可以注册
            //存储新的用户名和密码
            //进行本地存储
            SaveRegisterData();
            //代表 注册成功
            return true;
        }
        
        //验证用户名密码是否合法

        public bool CheckInfo(string username, string password)
        {
            //判断是否有该用户
            if (_registerData.RegisterDataDic.ContainsKey(username))
            {
                //密码相同，证明 登录成功
                if (_registerData.RegisterDataDic[username] == password)
                {
                    return true;
                }
            }
            //用户名与密码不合法
            return false;
        }

        #endregion
        
    }
}