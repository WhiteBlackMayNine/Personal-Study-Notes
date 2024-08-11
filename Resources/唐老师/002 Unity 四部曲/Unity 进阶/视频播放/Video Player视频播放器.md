---
tags:
  - 科学/Unity/唐老狮/Unity进阶/视频播放/VideoPlayer视频播放器
created: 2024-08-03
更新:
---

---
# 关联知识点



---
# Video Player 是什么

- Video Player顾名思义是视频播放器的意思
- 它是Unity提供给我们用于播放视频的组件
- 该视频播放器组件
	- 可以在游戏中播放导入的视频剪辑文件
# 添加 Video Player 组件
## 方法一

- 在Hierarchy窗口点击加号，选择Video ——> VideoPlayer
## 方法二

- 选择场景上任何一个对象，为其添加 Video Player 组件
## 方法三

- 直接将视频文件拖入到 Hierarchy 窗口中
# Video Player 参数相关
## Source 

- 视频源
### Video Clip

- 视频剪辑
- 可以直接将视频剪辑拖入此处或者选择视频剪辑进行关联
### URL

- 视频路径
- 选择视频的路径，可以是远程视频路径也可以之后通过代码直接关联视频资源路径
## Play On Awake

- 场景启动时就播放视频
- 如果希望自己控制播放时机，请取消该选项
## Wait For First Frame

- 如果勾选该选项
	- Unity 将等待视频第一帧准备好显示
- 如果取消勾选
	- 可能会丢弃前几帧
	- 使视频时间与游戏保持同步

- 视频播放的时候，一开始可能会稍微准备很短的一个时间
- 在后台去进行了一些计算，准备了关于视频的一些数据
- 如果等待（勾选），就等待这些数据准备完了，然后从头开始播放
## Render Mode 

- 渲染方式
- 决定视频的画面渲染在哪里
### Camera Far Plane

- 在摄像机的远平面上渲染
- 点击摄像机对象，最大最远的那个平面
- 如果在远平面 到 近平面 之间存在物体，点击开始时，Game 窗口 上能看的该物体
#### Camera

- 定义接受视频的摄像机
#### Alpha

- 视频的全局透明度
- 值越小，透明度越高
#### 3D Layout

- [[全景视频]] 才会去修改
### Camera Near Plane

- 在摄像机的近平面上渲染
### Render Texture

- 将视频渲染到 渲染纹理 中
	- 相当于一张底片
	- Creat ——> Render Texture
- 建议 尺寸 跟视频的 分辨率 一致
- 可以把 渲染纹理 放到 UI 上
#### Target Texture

- 用于渲染图像的渲染纹理
### Material Override

- 通过游戏对象渲染器的材质
- 将视频渲染到游戏对象的选定纹理属性中
#### Renderer

- 把视频渲染到某个 3D物品 上时
	- 可以使用这个
	- 3D物品需要添加对应的 Renderer 组件
- 用于渲染图像的渲染器如果为 None
- 则使用 Video Player 依附对象的上的渲染器
#### API Only

- 不预先设置渲染到哪里
- 通过代码来设置视频渲染到哪里
-  通过Video Player中的 texture属性 进行设置
## Aspect Ratio

- 宽高比设置
### No Scaling

- 不使用缩放
### Fit Vertically

- 垂直适应目标矩形
- 必要时裁剪左侧和右侧或在每侧留下黑色区域
- 以保留原视频的宽高比
### Fit Horizontally

- 水平适应目标矩形
- 必要时裁剪顶部和底部或在顶或底留下黑色区域
- 以保留原视频的宽高比
### Fit Inside

- 对原视频进行缩放而不必裁剪
- 但是可能会留黑边
### Fit Outside

- 对原视频进行缩放，可能需要进行裁剪，但是不会留黑边
### Stretch

- 在水平和垂直方向均进行缩放以适应目标矩形
	- 不会保留源宽高比
## Audio Output Mode

- 如何输出视频源的音频
### None

- 不播放音频
### Audio Source

- 发送给指定的音频源对象，允许 Unity 的音频处理
### Direct

- 直接绕过Unity音频处理，发送给音频输出硬件输出

-  Mute
	- 静音
- Volume
	- 音量
### API Only

- 通过代码将音频样本发送到关联的 AudioSampleProvider 听诊器
# Video Player 代码相关
## 注意

- 使用 VideoPlayer 组件需要引用命名空间 `UnityEngine.Video`

- 将一个 VideoPlayer 附加到主摄像机
- 将 VideoPlayer 添加到摄像机对象时
- 它会自动瞄准摄像机背板，无需更改 `videoPlayer.targetCamera`

```C#
GameObject camera = GameObject.Find("Main Camera");
videoPlayer = camera.AddComponent<VideoPlayer>();
```
## 关联变量

```C#
    public RenderTexture texture;//渲染纹理

    public VideoClip clip;//视频切片

    VideoPlayer videoPlayer;//VideoPlayer 对象 Update中方法的调用

    private bool isOver = false;//是否准备完成
```
## 参数相关设置

- 点运算符 点出 Inspector 的参数
```C#
void Start{

	//  是否自动播放
	videoPlayer.playOnAwake = false;
	
	//  渲染模式
	videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
	
	//设置目标 渲染贴图
	videoPlayer.targetTexture = texture;
	
	//设置目标摄像机
	videoPlayer.targetCamera
	
	//  透明度
	videoPlayer.targetCameraAlpha = 0.5f;
	videoPlayer.targetCamera3DLayout = Video3DLayout.OverUnder3D;
	
	//  视频源
	videoPlayer.source = VideoSource.VideoClip;
	videoPlayer.clip = clip;
	
	videoPlayer.source = VideoSource.Url;
	//把视频文件放到 StreamingAssets 流文件夹下
	videoPlayer.url = Application.streamingAssetsPath + "/Video.mp4";
	
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

}
```
### 方法、事件相关

```C#
void Start{

	//  播放、停止、暂停
	//在 viod Update 中
	
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

}

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
```


# 总结

- VideoPlayer是用于视频播放的组件
- 我们可以通过它来控制视频播放相关设置

- 其中比较重要的参数有
	- 渲染模式
		- 决定视频渲染到哪里
	- 播放暂停相关 API
		- 制作 UI 相关的内容、功能

- 视频播放时，需要有短暂的准备时间才会开始播放
- 我们可以通过 Prepare 函数来启动准备，准备完毕后再正式开始播放

---
# 源代码

- ![[Lesson2视频播放.cs]]

---