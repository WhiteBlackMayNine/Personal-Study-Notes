using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 为什么要对视频剪辑进行设置？
        //当我们将准备好的视频导入Unity后
        //我们可以选中该视频剪辑，并在Inspector窗口中进行设置
        //设置视频剪辑的主要原因
        //1.预览视频效果
        //2.查看视频文件的基本信息
        //3.设置视频剪辑是否开启转码设置（重要）

        //通过对视频剪辑进行转码相关设置
        //我们可以保证在各平台都能正常播放
        #endregion

        #region 知识点二 视频剪辑设置参数相关

        #endregion

        #region 总结
        //视频剪辑相关设置的主要目的
        //是为了设置视频转码相关方案
        //通过视频转码，可以让视频尽量支持各平台

        //关于各平台的转码规则 我们在上节课中就已经讲过
        //1.可用于硬件加速并且本机支持的最优视频编解码器是 H.264
        //2.当优先考虑跨平台支持时，VP8 是一个不错的选择。
        //  VP8 得到广泛支持并具有全面的功能集，但与硬件加速的编解码器（例如 H.264）相比，需要消耗更多的资源。
        //3.在支持 H.265 的设备上可以使用 H.265。
        //4.Android 使用原生库支持 VP8，因此 VP8 在某些 Android 设备上也可能获得硬件辅助。

        //一劳永逸 mp4格式 + H.264编解码器
        //根据设备情况考虑使用 VP8 和 H.265 编解码器
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
