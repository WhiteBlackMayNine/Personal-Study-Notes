---
tags:
  - 科学/Unity/Unity入门/重要组件和API/输入相关Input
created: 2024-03-23
课时: 42 43
来源 - 42: https://www.bilibili.com/video/BV1HX4y1V71E?p=26
来源 - 43: https://www.bilibili.com/video/BV1HX4y1V71E?p=27
备注: 输入相关内容肯定写在 Update 中的，且一般与 条件分支语句 连用进行判断
---

---
# 关联知识点

[[生命周期函数]] [[变量]]

---
# 知识点
## 鼠标在屏幕中的位置

### 获取（只有 get）

- `Input.mousePosition`
- 屏幕是个二维，原点在左下角，往右为 X ，往上为 Y
- 返回值是 Vector3 ，但只能返回 x 和 y 的值

## 检测鼠标输入

### 鼠标按下一瞬间

- `Input.GetMouseButtonDown( 0/1/2 )
- 返回值为 **布尔值** 
### 鼠标抬起一瞬间

- `Input.GetMouseButtonUp( 0/1/2 )`
- 返回值为 **布尔值**
### 鼠标长按、按下、抬起都会进入

- `Input.GetMouseButton( 0/1/2 )`
- 返回值为 **布尔值** 
- 当一直按住不放，会一直触发这个判断
### 中键滚动

- `Input.mouseScrollDelta`
- 返回值是 Vector2 ，但只会改变其中的 Y 值
- -1 为往下滚，1为往上滚，0表示不滚
### 左键为 0 ，右键为 1 ，中键为 2
 
## 检测键盘输入

### 按下

#### API（推荐）

- `Input.GetKeyDown(KeyCode.Text);`
	- Text 为按键名
	- 返回值为 **布尔值
#### 传入字符串的重载（不推荐）

- 只能传入小写的，否则会报错
- `Input.GetKeyDown("q");`
### 抬起

- `Input.GetKeyUp(KeyCode.Text);`
	- Text 为按键名
	- 返回值为 **布尔值
### 长按

- `Input.GetKey(KeyCode.Text);`
	- Text 为按键名
## 检测默认轴输入

### 默认轴数

- Unity 提供了简便的方法来控制对象的位移和旋转
### API 

- `Input.GetAxis("Text")`
- Text 可在 编辑—项目管理—输入管理 中查看
- 也就是下面的这几个英文单词
### 键盘AD按下 返回 -1 到 1 之间变换

- `Horizontal
- 得到的值就是左右方向，可以通过它来控制对象左右移动或旋转
### 键盘SW按下 返回 -1 到 1 之间变换

- `Vertical
- 上下方向，控制上下移动或旋转
### 鼠标横向移动 返回 -1 到 1 （左    右）

- `Mouse X
### 鼠标竖向移动 返回 -1 到 1 （上    下）

- `Mouse Y
### 注

- `GetAxisRaw`与`GetAxis`使用方法相同
- 只不过前者返回值只是 1、0、-1，不会有中间值
- 后者为渐变

## 其他

### 键鼠相关

#### 是否有任意键或鼠标长按

- `Input.anyKey`
- 返回值为**布尔值**
#### 是否有任意键或鼠标按下

- `Input.anyKeyDown`
#### 这一帧的键盘输入

- `Input.inputString`
### 手柄相关

#### 得到连接的手柄的所有按钮名字
- `string[] str = Input.GetJoystickNames(); `
#### 某一个手柄键按下

- `Input.GetButtonDown("Text")`
- Text 为按键名
#### 某一个手柄键抬起

- `Input.GetButtonUp("Text")`
- Text 为按键名
#### 某一个手柄键长按

- `Input.GetButton("Text")`
- Text 为按键名


---
