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
        }
        
        //为了直接管理数据
        //避免外部进行修改，不让外部去 new
        private LoginData _loginData;
        public LoginData LoginData => _loginData ??= new LoginData();

        #region 登录数据

        /// <summary>
        /// 存储登录相关数据
        /// </summary>
        public void SaveLoginData()
        {
            JsonMgr.Instance.SaveData(_loginData, "LoginData");
        }

        #endregion
        
    }
}