# 提前声明

- 写这个的时候还没有学 Addressable
- 只是把如何打包整了一下

- 备注
	- 打包完之后的场景无法改变
# 具体方法

- ![[2024TapTap聚光灯 Addressable 打包 Groups 界面打开方法.png]]
- ![[2024TapTap聚光灯 Addressable 打包 Use Existing Build 选择.png]]
- ![[2024TapTap聚光灯 Addressable 打包 生成默认代码.png]]
- ![[2024TapTap聚光灯 Addressable 打包 init初始化场景创建 挂载脚步 .png]]

- 脚本内容
```C#
public AssetReference persistentScene;  
  
private void Awake()  
{  
    Addressables.LoadSceneAsync(persistentScene);  
}
```