using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson8 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Text是什么
        //Text是文本组件
        //是UGUI中用于显示文本的关键组件
        #endregion

        #region 知识点二 Text参数相关

        #endregion

        #region 知识点三 富文本

        #endregion

        #region 知识点四 边缘线和阴影
        //边缘线组件 outline

        //阴影组件 Shadow
        #endregion

        #region 知识点五 代码控制
        Text txt = this.GetComponent<Text>();
        txt.text = "唐老狮 哈哈哈哈哈";
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
