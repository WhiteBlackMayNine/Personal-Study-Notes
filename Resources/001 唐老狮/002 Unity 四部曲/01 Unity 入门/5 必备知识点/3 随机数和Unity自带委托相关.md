---
tags:
  - 科学/Unity/唐老狮/Unity入门/随机树和Unity自带委托相关
created: ""
---

---
# 关联知识点



---
# 随机数
## Unity 的随机数

- Unity中的随机数
	- Unity当中的 Random 类
	- 此Random(Unity)非彼Random(C#)

- Unity专门有一个类 Random，在 UnityEngine 命名空间中
- 这个类的使用，跟 CSharp 就不一样了

```C#
//两个 Int 或 两个 float 进行取随机
//min 与 max 的类型必须一样
//int 重载 为 左包含右不包含 
//float 重载 为 左包含右也包含
int / float randomNum = Random.range(min,max);
```
## CSharp 的随机数

```C#
System.Random r = new System.Random();
r.Next(0,100);
```
# 委托
## CSharp 的自带委托

```C#
//无返回值
System.Action<int float> action = (i,f)=>{
	print(i + f);
};
//有返回值
//泛型 int ——> 返回值类型为 int
System.Func<int> func = ()=>{
	return 1;
}
//传入一个 int 进去
//返回一个 float
System.Func<int,float> func = (i)=>{
	return 1.5f;
}
//Func ——> 最后一个为返回值类型
```
## Unity 的自带委托

- 需要引用命名空间

- 无参无返回
- 跟 Action 使用方法类似
- 备注： [[15-受伤和死亡的逻辑和动画|麦扣老师 勇士传说]] 中使用了 Unity自带委托

```C#
UnityAction uc = ()=>{

};

UnityAction<int> uc1 = (i)=>{
	
};
```
## 委托作用

- 无论是 CSharp 还是 Unity 的委托
- 主要作用就是
	- ——> 函数的容器
	- ——> 用来存储函数
	- ——> 可以在函数之间传递函数
# 总结

- 委托如果无返回值，就用 UnityAction
- 如果有返回值，就用 CSharp 的 Func

- 随机数就用 Unity 的 Random

---
