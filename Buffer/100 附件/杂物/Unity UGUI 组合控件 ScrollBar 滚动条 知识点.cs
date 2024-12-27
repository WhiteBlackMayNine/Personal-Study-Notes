using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson14 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Scrollbar是什么
        //Scrollbar是滚动条组件
        //是UGUI中用于处理滚动条相关交互的关键组件

        //默认创建的Scrollbar由2组对象组成
        //父对象——Scrollbar组件依附的对象
        //子对象——滚动块对象

        //一般情况下我们不会单独使用滚动条 
        //都是配合ScrollView滚动视图来使用
        #endregion

        #region 知识点二 Scrollbar参数

        #endregion

        #region 知识点三 代码控制
        Scrollbar sb = this.GetComponent<Scrollbar>();
        print(sb.value);
        print(sb.size);
        #endregion

        #region 知识点四 监听事件的两种方式
        //1.拖脚本
        //2.代码添加
        sb.onValueChanged.AddListener((v) => {
            print("代码监听的函数" + v);
        });
        #endregion
    }

    public void ChangeValue(float v)
    {
        print(v);
    }
}
