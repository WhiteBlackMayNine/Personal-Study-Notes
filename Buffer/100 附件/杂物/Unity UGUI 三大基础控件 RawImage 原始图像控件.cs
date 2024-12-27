using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson9 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 RawImage是什么
        //RawImage是原始图像组件
        //是UGUI中用于显示任何纹理图片的关键组件

        //它和Image的区别是 一般RawImage用于显示大图(背景图，不需要打入图集的图片，网络下载的图等等)
        #endregion

        #region 知识点二 RawIamge参数

        #endregion

        #region 知识点三 代码控制
        RawImage raw = this.GetComponent<RawImage>();
        raw.texture = Resources.Load<Texture>("ui_TY_lvseshuzi_08");
        raw.uvRect = new Rect(0, 0, 1, 1);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
