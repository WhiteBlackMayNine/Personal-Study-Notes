using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Login.Panel
{
    public class TipPanel : BasePanel
    {
        //确定按钮
        public Button sureButton;
        //提示文字
        public Text tipText;
        
        public override void Init()
        {
            //进行初始化 (包括 隐藏自己)
            sureButton.onClick.AddListener(() =>
            {
                //通过UI管理器 隐藏自己
                UIManager.Instance.HidePanel<TipPanel>();
            } );
        }

        /// <summary>
        /// 改变提示内容 
        /// </summary>
        /// <param name="tip"></param>
        public void ChangeTip(string tip)
        {
            tipText.text = tip;
        }
    }
}