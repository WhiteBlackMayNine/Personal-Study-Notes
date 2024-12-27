using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson24 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 自动布局是什么
        //虽然UGUI的RectTransform已经非常方便的可以帮助我们快速布局
        //但UGUI中还提供了很多可以帮助我们对UI控件进行自动布局的组件
        //他们可以帮助我们自动的设置UI控件的位置和大小等

        //自动布局的工作方式一般是
        //自动布局控制组件 + 布局元素 = 自动布局

        //自动布局控制组件：Unity提供了很多用于自动布局的管理性质的组件用于布局
        //布局元素：具备布局属性的对象们，这里主要是指具备RectTransform的UI组件
        #endregion

        #region 知识点二 布局元素的布局属性
        //要参与自动布局的布局元素必须包含布局属性
        //布局属性主要有以下几条
        //Minmum width：该布局元素应具有的最小宽度
        //Minmum height：该布局元素应具有的最小高度

        //Preferred width：在分配额外可用宽度之前，此布局元素应具有的宽度
        //Preferred height：在分配额外可用高度之前，此布局元素应具有的高度。

        //Flexible width：此布局元素应相对于其同级而填充的额外可用宽度的相对量
        //Flexible height：此布局元素应相对于其同级而填充的额外可用高度的相对量

        //在进行自动布局时 都会通过计算布局元素中的这6个属性得到控件的大小位置

        //在布局时，布局元素大小设置的基本规则是
        //1.首先分配最小大小Minmum width和Minmum height
        //2.如果父类容器中有足够的可用空间，则分配Preferred width和Preferred height
        //3.如果上面两条分配完成后还有额外空间，则分配Flexible width和Flexible height

        //一般情况下布局元素的这些属性都是0
        //但是特定的UI组件依附的对象布局属性会被改变，比如Image和Text

        //一般情况下我们不会去手动修改他们，但是如果你有这些需求
        //可以手动添加一个LayoutElement组件 可以修改这些布局属性
        #endregion

        #region 知识点三 水平垂直布局组件
        //水平垂直布局组件
        //将子对象并排或者竖直的放在一起

        //组件名：Horizontal Layout Group 和 Vertical Layout Group
        //参数相关：
        //Padding：左右上下边缘偏移位置
        //Spacing:子对象之间的间距

        //ChildAlignment:九宫格对其方式
        //Control Child Size：是否控制子对象的宽高
        //Use Child Scale：在设置子对象大小和布局时，是否考虑子对象的缩放
        //Child Force Expand：是否强制子对象拓展以填充额外可用空间
        #endregion

        #region 知识点四 网格布局组件
        //网格布局组件
        //将子对象当成一个个的格子设置他们的大小和位置

        //组件名：Grid Layout Group
        //参数相关：
        //Padding：左右上下边缘偏移位置
        //Cell Size：每个格子的大小
        //Spacing：格子间隔
        //Start Corner:第一个元素所在位置（4个角）
        //Start Axis：沿哪个轴放置元素；Horizontal水平放置满换行，Vertical竖直放置满换列
        //Child Alignment：格子对其方式（9宫格）
        //Constraint：行列约束
        //  Flexible：灵活模式，根据容器大小自动适应
        //  Fixed Column Count：固定列数
        //  Fixed Row Count：固定行数
        #endregion

        #region 知识点五 内容大小适配器
        //内容大小适配器
        //它可以自动的调整RectTransform的长宽来让组件自动设置大小
        //一般在Text上使用 或者 配合其它布局组件一起使用

        //组件名：Content Size Fitter
        //参数相关
        //Horizontal Fit：如何控制宽度
        //Vertical Fit:如何控制高度

        //Unconstrained：不根据布局元素伸展
        //Min Size：根据布局元素的最小宽高度来伸展
        //Preferred Size：根据布局元素的偏好宽度来伸展宽度。
        #endregion

        #region 知识点六 宽高比适配器
        //宽高比适配器
        //1.让布局元素按照一定比例来调整自己的大小
        //2.使布局元素在父对象内部根据父对象大小进行适配

        //组件名：Aspect Ratio Fitter
        //参数相关：
        //Aspect Mode：适配模式，如果调整矩形大小来实施宽高比
        //  None：不让矩形适应宽高比
        //  Width Controls Height：根据宽度自动调整高度
        //  Height Controls Width：根据高度自动调整宽度
        //  Fit In Parent：自动调整宽度、高度、位置和锚点，使矩形适应父项的矩形，同时保持宽高比，会出现“黑边”
        //  Envelope Parent：自动调整宽度、高度、位置和锚点，使矩形覆盖父项的整个区域，同时保持宽高比，会出现“裁剪”

        //Aspect Ratio：宽高比；宽除以高的比值
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
