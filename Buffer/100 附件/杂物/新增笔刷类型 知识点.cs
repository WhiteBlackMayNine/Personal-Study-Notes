using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson27 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 新建自定义笔刷
        //1.预设体笔刷——用于快捷刷出想要创建的精灵
        //2.预设体随机笔刷——用于快捷随机刷出想要创建的精灵
        //3.随机笔刷——可以指定瓦片进行关联，随机刷出对应瓦片
        #endregion

        #region 知识点二 拓展笔刷
        //1. Coordinate Brush 坐标笔刷 —— 可以实时看到格子坐标
        //2. GameObject Brush 游戏对象笔刷 —— 可以在场景中选择和擦除游戏对象仅限于选定的游戏对象的子级
        //3. Group Brush 组合笔刷 —— 可以设置参数 当点击一个瓦片样式时 会自动取出一个范围内的瓦片
        //4. Line Brush 线性笔刷 —— 决定起点和终点画一条线出来
        //5. Random Brush 随机笔刷 —— 和之前的自定义随机画笔类似，可以随机画出瓦片
        //6. Tint Brush 着色笔刷 —— 可以给瓦片着色 瓦片的颜色锁要开启（Inspector窗口切换Debug模式 修改Flags）
        //7. Tint Brush(Smooth) 光滑着色笔刷 —— 可以给瓦片进行渐变着色，需要按要求改变材质
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
