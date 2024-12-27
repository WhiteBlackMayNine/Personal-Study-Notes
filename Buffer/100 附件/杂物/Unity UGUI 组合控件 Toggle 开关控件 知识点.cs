using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson11 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Toggle是什么
        //Toggle是开关组件
        //是UGUI中用于处理玩家单选框多选框相关交互的关键组件

        //开关组件 默认是多选框
        //可以通过配合ToggleGroup组件制作为单选框

        //默认创建的Toggle由4个对象组成
        //父对象——Toggle组件依附
        //子对象——背景图（必备）、选中图（必备）、说明文字（可选）
        #endregion

        #region 知识点二 Toggle参数

        #endregion

        #region 知识点三 代码控制
        Toggle tog = this.GetComponent<Toggle>();
        tog.isOn = true;
        print(tog.isOn);

        ToggleGroup togGroup = this.GetComponent<ToggleGroup>();
        togGroup.allowSwitchOff = false;

        //可以遍历提供的迭代器 得到当前处于选中状态的 Toggle
        foreach (Toggle item in togGroup.ActiveToggles())
        {
            print(item.name + " " + item.isOn);
        }
        #endregion

        #region 知识点四 监听事件的两种方式
        //1.拖脚本
        //2.代码添加
        tog.onValueChanged.AddListener(ChangeValue2);
        tog.onValueChanged.AddListener((b) =>
        {
            print("代码监听 状态改变" + b);
        });
        #endregion
    }

    public void ChangValue(bool isOn)
    {
        print("状态改变" + isOn);
    }    

    private void ChangeValue2(bool v)
    {
        print("代码监听 状态改变" + v);
    }
}
