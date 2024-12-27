using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson12 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 InputField是什么
        //InputField是输入字段组件
        //是UGUI中用于处理玩家文本输入相关交互的关键组件

        //默认创建的InputField由3个对象组成
        //父对象——InputField组件依附对象 以及 同时在其上挂载了一个Image作为背景图
        //子对象——文本显示组件（必备）、默认显示文本组件（必备）
        #endregion

        #region 知识点二 InputField参数

        #endregion

        #region 知识点三 代码控制
        InputField input = this.GetComponent<InputField>();
        print(input.text);
        input.text = "123123123123";
        #endregion

        #region 知识点四 监听事件的两种方式
        //1.拖脚本
        //2.代码添加
        input.onValueChanged.AddListener((str) =>
        {
            print("代码监听 改变" + str);
        });

        input.onEndEdit.AddListener((str) =>
        {
            print("代码监听 结束输入" + str);
        });
        #endregion
    }

    public void ChangeInput(string str)
    {
        print("改变的输入内容" + str);
    }

    public void EndInput(string str)
    {
        print("结束输入时内容" + str);
    }
}
