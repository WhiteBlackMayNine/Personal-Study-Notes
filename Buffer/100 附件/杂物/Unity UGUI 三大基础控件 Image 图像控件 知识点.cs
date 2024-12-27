using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson7 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Image是什么？
        //Image是图像组件
        //是UGUI中用于显示精灵图片的关键组件
        //除了背景图等大图，一般都使用Image来显示UI中的图片元素
        #endregion

        #region 知识点二 Image参数

        #endregion

        #region 知识点三 代码控制
        Image img = this.GetComponent<Image>();
        img.sprite = Resources.Load<Sprite>("ui_TY_fanhui_01");

        (transform as RectTransform).sizeDelta = new Vector2(200, 200);
        img.raycastTarget = false;

        img.color = Color.red;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
