using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Lesson2 : MonoBehaviour
{
    public RenderTexture texture;

    public VideoClip clip;

    VideoPlayer videoPlayer;

    private bool isOver = false;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Video Player是什么
        //Video Player顾名思义是视频播放器的意思
        //它是Unity提供给我们用于播放视频的组件
        //该视频播放器组件
        //可以在游戏中播放导入的视频剪辑文件
        #endregion

        #region 知识点二 添加Video Player组件
        //方法一：在Hierarchy窗口点击加号，选择Video > Video Player
        //方法二：选择场景上任何一个对象，为其添加Video Player组件
        //方法三：直接将视频文件拖入到Hierarchy窗口中
        #endregion

        #region 知识点三 Video Player参数相关

        #endregion

        #region 知识点四 Video Player代码相关
        //注意：使用VideoPlayer组件需要引用命名空间UnityEngine.Video

        //1.将一个 VideoPlayer 附加到主摄像机。
        //  将 VideoPlayer 添加到摄像机对象时，
        //  它会自动瞄准摄像机背板，无需更改 videoPlayer.targetCamera。
        GameObject camera = GameObject.Find("Main Camera");
        videoPlayer = camera.AddComponent<VideoPlayer>();

        //2.参数相关设置
        //  是否自动播放
        videoPlayer.playOnAwake = false;
        //  渲染模式
        videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
        //设置目标 渲染贴图
        //videoPlayer.targetTexture = texture;
        //设置目标摄像机
        //videoPlayer.targetCamera

        //  透明度
        videoPlayer.targetCameraAlpha = 0.5f;
        //videoPlayer.targetCamera3DLayout = Video3DLayout.OverUnder3D;

        //  视频源
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = clip;

        //videoPlayer.source = VideoSource.Url;
        //videoPlayer.url = Application.streamingAssetsPath + "/Video.mp4";

        //  是否循环
        videoPlayer.isLooping = false;

        //  视频总时长
        print(videoPlayer.length);//单位为s
        //  当前时长,播放了多久
        print(videoPlayer.time);//单位为s

        //  总帧数
        print(videoPlayer.frameCount);
        //  当前帧
        print(videoPlayer.frame);


        //3.方法相关
        //  播放、停止、暂停

        //  准备函数
        videoPlayer.Prepare();

        //4.事件相关
        //  准备完成事件
        videoPlayer.prepareCompleted += (v) =>
        {
            print("准备完成");
            isOver = true;
        };

        //  开始事件
        videoPlayer.started += (v) =>
        {
            print("当执行player播放方法后 会调用的事件");
        };

        //  结尾时调用事件
        videoPlayer.loopPointReached += (v) =>
        {
            print("视频播放到结尾处时会调用的事件");
        };
        #endregion

        #region 总结
        //VideoPlayer是用于视频播放的组件
        //我们可以通过它来控制视频播放相关设置

        //其中比较重要的参数有
        //1.渲染模式
        //2.播放暂停相关API

        //视频播放时，需要有短暂的准备时间才会开始播放
        //我们可以通过Prepare函数来启动准备，准备完毕后再正式开始播放
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOver)
        {
            //视频播放
            videoPlayer.Play();
        }

        if (Input.GetKeyDown(KeyCode.S) && isOver)
        {
            //视频停止
            videoPlayer.Stop();
        }

        if (Input.GetKeyDown(KeyCode.P) && isOver)
        {
            //视频暂停
            videoPlayer.Pause();
        }
    }
}
