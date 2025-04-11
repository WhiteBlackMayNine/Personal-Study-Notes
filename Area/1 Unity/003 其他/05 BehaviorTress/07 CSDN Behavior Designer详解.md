---
tags:
  - "#Unity"
  - "#BehaviorTree"
created: 2025-04-03
来源: https://blog.csdn.net/qq_52324195/article/details/124827915
---

---
# 关联知识点



---
# 行为树的基本原理

- 行为树包含逻辑节点与行为节点，每次需要一个行为时，会从树的根节点出发，遍历各个节点
	- 找出第一个和当前数据相符合的节点

- 当我们要决策一个AI要做什么样的行为的时候， **我们就会自顶向下的，通过一些条件来搜索这颗树，最终确定需要做的行为（叶节点），并且执行它**，这就是行为树的基本原理
# Task & Status

- 总计有四种不同的 task
	- Action 行为
	- Composite 复合
	- Conditional 条件
	- Decorator 装饰
##  **复合（Composites）**

- Sequence
    - 顺序执行，会按照从左向右的顺序依次执行子节点
- Selector
    - 选择执行，根据条件判断，选择一个子节点执行
- Parallel
    - 平行执行，同时执行所有子节点

- 请注意
	- 如果子节点中有一个节点报错，那么 复合Task便会返回 Failure
	- 后面的子节点也不会去执行
## **修饰（Decorator）**

- 只能有一个子节点，修改子节点的行为
## **条件（Conditional）**

- 用来进行判断某些值（或者行为）是否合适
## **行为（Action）**

- 具体的动作
- 改变游戏对象的状态与结果
## **返回状态（Status）**

- 如果一个任务节点（Task）需要多帧才能完成
	- 比如说动画
- 或者 Conditional 需要一种方法告诉父节点条件是否正确
- 这种情况下都可以使用 状态Status 来解决

- 一个任务有三种状态
	- 运行
	- 成功
	- 失败

- 状态是任务的 *"返回值”*
# 行为树整体的编辑界面
## Behavior

- Behavior Name
	- 行为树名称
- Behavior Description
	- 行为树简介
- Extenral Description
	- 外部行为树
		- 关于这一属性，可以通过右上角的Export将行为树导出
		- 创建一个行为树“Prefab”，他就成为了外部行为树
		- 然后将这个外部行为树拖入该属性，就会将 外部行为树 应用到 当前行为树
- Group
	- 行为树的分组编号，用来将行为树分组！
	- 可以用来方便的查找到特定的行为树
- Start When Enabled
	- 如果设置为 true，那么当这个行为树组件 enabled 的时候，这个行为树就会被执行
- Asynchronous Load
	- 异步加载
- Pause When Disabled
	- 如果设置为 true，那么当这个行为树组件 disabled 的时候，这个行为树就会被暂停
- Restart When Complete
	- 如果设置为 true，那么当这个行为树组件执行结束的时候，这个行为树就会被重新执行
- Reset Values On Restart
	- 如果设置为 true，那么当这个行为树组件 reset 的时候，这个行为树就会被重新执行
- Log Task Changes
	- 当设置为 true 是，这个行为树下只要 task 流程发生变化就会打印一条 log 日志到控制台中
## Variable

- 全局变量
## Inspector

- 跟Unity的Inspector窗口类似
- 点击任意的 Task 后，Inspector窗口便会显示其参数
## Task

- 在整个任务树的最高层的节点我们称之为 Task（任务）
- 这些 Task 任务拥有类似于 MonoBehavior 那样的接口用于实现和扩展
### 基础公共属性

- Name
- Comment
- Instant

- Instant
	- 行为树中，当一个 task 任务返回成功或者失败后，行为树会在同一帧中立刻移动到下一个 task 任务
	- 如果，你没有选择 instant 选项，那么在当前 task 任务执行完毕后，都会停留在当前节点中
		- 直到收到了下一个 tick，才会移动到下一个 task 任务

- tick 就涉及到 BehaviorManager
	- 简单来说就是 行为树更新指令

#### BehaviorManager

- 运行一个行为树的时候，会在场景中自动创建一个名称为 BehaviorManager的 GameObject，
	- 并添加 BehaviorManage.cs

- 参数
	- Update Interval
		- 行为树的更新逻辑
			- Every Frame 每帧更新
			- Specify Seconds 自定义时间更新
			- Manual 手动更新
	- Task Execution Type
		- 指定这次更新中行为树的执行次数
		- 默认是`No Dullicates`
			- 也就是无复制无重复的意思，也就是 1次，每次更新行为树，行为树会执行一次

- 如果参数选择了 Manual
	- 可以使用代码 `BehaviorManager.instance.Tick()` 更新所有行为树
		- 重载：`BehaviorManager.instance.Tick(BehaviorTree);` 更新一个行为树
	- ![[07 CSDN Behavior Designer详解 行为树执行顺序流程图.png]]

## API

```C#
// 当行为树被启用时，OnAwake被调用一次。可以把它看作一个构造函数
public virtual void OnAwake();
// OnStart在执行之前立即被调用。它用于设置需要在上次运行后重新设置的任何变量
public virtual void OnStart();
// OnUpdate运行实际的任务
public virtual TaskStatus OnUpdate();
// 在执行成功或失败后调用OnEnd。
public virtual void OnEnd();
// 当行为暂停并恢复时，调用OnPause
public virtual void OnPause(bool paused);
// 优先级选择需要知道该任务的运行优先级
public virtual float GetPriority();
// OnBehaviorComplete在行为树完成执行后被调用
public virtual void OnBehaviorComplete();
// 检查器调用OnReset来重置公共属性
public propertiespublic virtual void OnReset();
// 允许从任务中调用OnDrawGizmos
public virtual void OnDrawGizmos();
// 保留对拥有此任务的行为的引用
public Behavior Owner;
```
## 父任务 Parent Tasks

- Behavior Tree 行为树中的父任务 Task 
	- Composite（复合），Decorator（修饰符）

```C#
//一个父任务可以拥有的子任务的最大数量。通常为1或int。MaxValue
public virtual int MaxChildren();
//布尔值，以确定当前任务是否为并行任务
public virtual bool CanRunParallelChildren();
//当前活动子节点的索引
public virtual int CurrentChildIndex();
//布尔值，以确定当前任务是否可以执行
public virtual bool CanExecute();
//为执行状态应用装饰器，输入参数为被修饰节点的状态
public virtual TaskStatus Decorate(TaskStatus status);
//通知parenttask子任务已被执行，其状态为childStatus
public virtual void OnChildExecuted(TaskStatus childStatus);
//通知父任务，其子任务childIndex已被执行，其状态为childStatus
public virtual void OnChildExecuted(int childIndex, TaskStatus childStatus);
//通知任务子进程已经开始运行
public virtual void OnChildStarted();
//通知并行任务，索引为childIndex的子任务已开始运行
public virtual void OnChildStarted(int childIndex);
//一些父任务需要能够覆盖状态，例如并行任务
public virtual TaskStatus OverrideStatus(TaskStatus status);
//如果中断节点被中断，它将覆盖状态。
public virtual TaskStatus OverrideStatus();
//通知复合任务，条件中止已被触发，子索引应重置
public virtual void OnConditionalAbort(int childIndex);
```
# 条件节点的终止

- 所有的条件节点都有一个属性
	- Abort Type
	- 也就是中止类型
- 四种中断类型
	- None
	- Self
	- Lower Priority
	- Both

- None
	- 无中止
- Self
	- 这是一种自包含中断类型
	- 也就是会检测此节点下所有条件判断节点
		- 即便是被执行过的节点
	- 如果判断条件不满足则打断当前执行顺序 重新 回到判断节点判断，并返回判断结果
	- ——> 简单来说 **当条件任务自身的结果变化时，中止自己的父节点的其他子任务**
		- 比如，在并行（Parallel）节点中，当某个条件任务的结果变化，可能中止其他正在运行的任务
- Lower Priority
	- 当运行到后续节点时，本节点的判断条件生效了的话则打断当前执行顺序，返回本节点执行
	- ——> 简单来说 **当条件任务的结果变化时，中止优先级较低的子任务**
		- 比如在选择器中，如果有高优先级的条件满足，中止当前正在执行的低优先级任务
- Both
	- 同时包含 Self 与 LowerPriority

- 中止只会发生在节点运行过程中
- 如果节点已经运行完毕，也就是出现了绿色的勾，此时说明执行完毕，行为树已经结束
- 中止就无效了
# Selector选择节点

- 首先说明Selector选择节点，这是系统默认的选择节点
- 可以理解为 if else，从左向右依次选择
	- 如果能执行就选择该子树执行，如果不能执行则选择下一个子树
- 注意，我们的用词，**子树**，也就是说，如果要执行下去，那么左边这个子树的条件节点必须返回 Success
	- 否则，就会执行下一个 **子树**
# 状态机与行为树的不同

- 行为树并非一种状态，它仅仅包含了一系列行为的逻辑模
- 一旦它执行完毕，不会向状态机一样执行完就回到 Idle，行为树就结束了
- 如果想要再次执行它，就需要再次通过脚本来启用
# 自定义行为节点

- 首先，需要引入命名空间
	```C#
	using BehaviorDesigner.Runtime;
	using BehaviorDesigner.Runtime.Tasks;
	```

- 对于行为节点，仅仅需要让该类继承 Action，然后重写 OnUpdate 函数即可
- 想要实现的功能就写在这个函数中，这里实现的就是让对象朝向目标移动
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MyMoveTo : Action
{
    public GameObject enemy;
    public override TaskStatus OnUpdate()
    {
        if (GetComponent<Player>())
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < 0.1f)
            {
                return TaskStatus.Success;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position,enemy.transform.position,Time.deltaTime);
                return TaskStatus.Running;
            }
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
```
# 自定义条件节点

- 同样引入命名空间，然后继承`Conditional`

- 判断敌人是否存活，如果否，那么返回`Failure`

```C#
public class MyConditional : Conditional
{
    public GameObject enemy;
    public override TaskStatus OnUpdate()
    {
        if (enemy.GetComponent<EnemyState>().alive)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
```
#  自定义修饰节点

- 继承`Decorator`

- 修饰节点比较特殊，这里重写了四个函数

- 第一个函数是CanExecute()，这个函数的返回值表明该节点能否通过

- 第二个函数是OnChildExecuted(TaskStatus childStatus)，输入参数为孩子节点状态，该函数会在孩子节点执行的时候调用

- 第三个函数为Decorate(TaskStatus status)，输入参数仍然是孩子节点状态，该函数的返回值会改变孩子节点的返回值

- 最后一个函数就没什么可说的了，这里我使用的行为树如下
	- ![[07 CSDN Behavior Designer详解 自定义修饰节点 使用行为树.png]]

- 首先通过Selector尝试调用Sequence
	- 当他调用到MyBreak的时候
	- 由于我们一开始的CanExecute通过条件为
	- `executionStatus == TaskStatus.Inactive || executionStatus == TaskStatus.Running`

- 也就是未激活或者正在运行时都允许通过
- 此时，通过MyBreak，走到MyConditional，然后判断MyConditional返回的状态
- 如果返回Success，此时通过 OnChildExecuted 检测到孩子节点状态为 Success
- 我们定义的变量 executionStatus 被赋予 Success，这导致CanExecute返回false
- 此时行为树从这条路中退出，能够继续运行，接下来继续执行MyMoveTo
- 这里有一点要注意，因为我们已经执行了条件判断，因此才可以执行MyMoveTo
- 我们之所以要在执行完条件判断后将该修饰节点的CanExecute返回false
- 是为了能够继续执行下去，如果这个函数一直返回true，
- 、这会导致一直执行条件判断，就无法继续执行MyMoveTo了
- 如果条件节点返回Failure那么该Sequence执行失败，Selector选择执行第二个Sequence。

- 这里要注意一点，中止导致的更新只有条件节点才能触发
- 并且在触发时只会遵循那时刻的所有节点的变量，如果你尝试更改其他节点使其无法执行，这是没有用的。只有条件节点才能触发此类行为

```C#
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MyBreak : Decorator
{
    private TaskStatus executionStatus = TaskStatus.Inactive;

    public override bool CanExecute()
    {
        Debug.Log(executionStatus == TaskStatus.Inactive || executionStatus == TaskStatus.Running);
        return executionStatus == TaskStatus.Inactive || executionStatus == TaskStatus.Running;
    }
    public override void OnChildExecuted(TaskStatus childStatus)
    {
        // Update the execution status after a child has finished running.
        executionStatus = childStatus;
    }
    public override TaskStatus Decorate(TaskStatus status)
    {
        if (GetComponent<Player>() && GetComponent<Player>().ready)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
    public override void OnEnd()
    {
        executionStatus = TaskStatus.Inactive;
    }
}

```
# 脚本创建行为树

- 文章后半段
- 感觉用不上这里就没写了



---
# 源代码



---