using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson13 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Slider是什么
        //Slider是滑动条组件
        //是UGUI中用于处理滑动条相关交互的关键组件

        //默认创建的Slider由4组对象组成
        //父对象——Slider组件依附的对象
        //子对象——背景图、进度图、滑动块三组对象
        #endregion

        #region 知识点二 Slider参数

        #endregion

        #region 知识点三 代码控制
        Slider s = this.GetComponent<Slider>();
        print(s.value);

        #endregion

        #region 知识点四 监听事件的两种方式
        //1.拖脚本
        //2.代码添加
        s.onValueChanged.AddListener((v) =>
        {
            print("代码添加的监听" + v);
        });
        #endregion
    }

    public void ChangeValue(float v)
    {
        print(v);
    }
}
