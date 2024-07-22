﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson22 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 物理系统之碰撞检测
        //碰撞产生的必要条件
        //1.至少一个物体有刚体
        //2.两个物体都必须有碰撞器

        //碰撞和触发
        //碰撞会产生实际的物理效果
        //触发看起来不会产生碰撞但是可以通过函数监听触发

        //碰撞检测主要用于实体物体之间产生物理效果时使用
        #endregion

        #region 知识点一 什么是范围检测
        //游戏中瞬时的攻击范围判断一般会使用范围检测
        //举例：
        //1.玩家在前方5m处释放一个地刺魔法，在此处范围内的对象将受到地刺伤害
        //2.玩家攻击，在前方1米圆形范围内对象都受到伤害
        //等等
        //类似这种并没有实体物体 只想要检测在指定某一范围是否让敌方受到伤害时 便可以使用范围判断
        //简而言之
        //在指定位置 进行 范围判断 我们可以得到处于指定范围内的 对象
        //目的是对 对象进行处理
        //比如 受伤 减血等等
        #endregion

        #region 知识点二 如何进行范围检测
        //必备条件：想要被范围检测到的对象 必须具备碰撞器
        //注意点：
        //1.范围检测相关API 只有当执行该句代码时 进行一次范围检测 它是瞬时的
        //2.范围检测相关API 并不会真正产生一个碰撞器 只是碰撞判断计算而已

        //范围检测API
        //1.盒状范围检测
        //参数一：立方体中心点
        //参数二：立方体三边大小
        //参数三：立方体角度
        //参数四：检测指定层级（不填检测所有层）
        //参数五：是否忽略触发器 UseGlobal-使用全局设置 Collide-检测触发器 Ignore-忽略触发器 不填使用UseGlobal
        //返回值：在该范围内的触发器（得到了对象触发器就可以得到对象的所有信息）
        print(LayerMask.NameToLayer("UI"));
        Collider[] colliders = Physics.OverlapBox( Vector3.zero, Vector3.one, Quaternion.AngleAxis(45, Vector3.up), 
                            1 << LayerMask.NameToLayer("UI") |
                            1 << LayerMask.NameToLayer("Default"), QueryTriggerInteraction.UseGlobal);

        for (int i = 0; i < colliders.Length; i++)
        {
            print(colliders[i].gameObject.name);
        }
        //0000 0001
        //0010 0000

        //重要知识点：
        //关于层级
        //通过名字得到层级编号 LayerMask.NameToLayer
        //我们需要通过编号左移构建二进制数
        //这样每一个编号的层级 都是 对应位为1的2进制数
        //我们通过 位运算 可以选择想要检测层级
        //好处 一个int 就可以表示所有想要检测的层级信息

        //层级编号是 0~31 刚好32位
        //是一个int数
        //每一个编号 代表的 都是二进制的一位
        //0—— 1 << 0——0000 0000 0000 0000 0000 0000 0000 0001 = 1
        //1—— 1 << 1——0000 0000 0000 0000 0000 0000 0000 0010 = 2
        //2—— 1 << 2——0000 0000 0000 0000 0000 0000 0000 0100 = 4
        //3—— 1 << 3——0000 0000 0000 0000 0000 0000 0000 1000 = 8
        //4—— 1 << 4——0000 0000 0000 0000 0000 0000 0001 0000 = 16
        //5—— 1 << 5——0000 0000 0000 0000 0000 0000 0010 0000 = 32

        //另一个API 
        //返回值：碰撞到的碰撞器数量
        //参数：传入一个数组进行存储
        //Physics.OverlapBoxNonAlloc()
        
        if(Physics.OverlapBoxNonAlloc(Vector3.zero, Vector3.one, colliders) != 0)
        {

        }

        //2.球形范围检测
        //参数一：中心点
        //参数二：球半径
        //参数三：检测指定层级（不填检测所有层）
        //参数四：是否忽略触发器 UseGlobal-使用全局设置 Collide-检测触发器 Ignore-忽略触发器 不填使用UseGlobal
        //返回值：在该范围内的触发器（得到了对象触发器就可以得到对象的所有信息）
        colliders = Physics.OverlapSphere(Vector3.zero, 5, 1 << LayerMask.NameToLayer("Default"));


        //另一个API 
        //返回值：碰撞到的碰撞器数量
        //参数：传入一个数组进行存储
        //Physics.OverlapSphereNonAlloc
        if( Physics.OverlapSphereNonAlloc(Vector3.zero, 5, colliders) != 0 )
        {

        }

        //3.胶囊范围检测
        //参数一：半圆一中心点
        //参数二：半圆二中心点
        //参数三：半圆半径
        //参数四：检测指定层级（不填检测所有层）
        //参数五：是否忽略触发器 UseGlobal-使用全局设置 Collide-检测触发器 Ignore-忽略触发器 不填使用UseGlobal
        //返回值：在该范围内的触发器（得到了对象触发器就可以得到对象的所有信息）
        colliders = Physics.OverlapCapsule(Vector3.zero, Vector3.up, 1, 1 << LayerMask.NameToLayer("UI"), QueryTriggerInteraction.UseGlobal);

        //另一个API 
        //返回值：碰撞到的碰撞器数量
        //参数：传入一个数组进行存储
        //Physics.OverlapCapsuleNonAlloc
        if ( Physics.OverlapCapsuleNonAlloc(Vector3.zero, Vector3.up, 1, colliders ) != 0 )
        {

        }
        #endregion

        #region 总结
        //范围检测主要用于瞬时的碰撞范围检测
        //主要掌握
        //Physics类中的静态方法
        //球形 盒装 胶囊三种API的使用即可
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
