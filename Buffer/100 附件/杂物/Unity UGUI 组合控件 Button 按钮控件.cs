using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson10 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Button是什么
        //Button是按钮组件
        //是UGUI中用于处理玩家按钮相关交互的关键组件

        //默认创建的Button由2个对象组成
        //父对象——Button组件依附对象 同时挂载了一个Image组件 作为按钮背景图
        //子对象——按钮文本（可选）
        #endregion

        #region 知识点二 Button参数

        #endregion


        #region 知识点三 代码控制
        Button btn = this.GetComponent<Button>();
        btn.interactable = true;
        btn.transition = Selectable.Transition.None;

        Image img = this.GetComponent<Image>();

        #endregion

        #region 知识点四 监听点击事件的两种方式
        //点击事件 是 在按钮区域抬起按下一次 就算一次点击

        //1.拖脚本

        //2.代码添加
        btn.onClick.AddListener(ClickBtn2);
        btn.onClick.AddListener(() => {
            print("123123123");
        });

        btn.onClick.RemoveListener(ClickBtn2);
        btn.onClick.RemoveAllListeners();
        #endregion
    }

    public void ClickBtn()
    {
        print("按钮点击，通过拖代码的形式");
    }

    private void ClickBtn2()
    {
        print("按钮点击，通过拖代码的形式2");
    }
}
