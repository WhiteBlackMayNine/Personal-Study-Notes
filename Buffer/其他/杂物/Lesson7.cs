using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson7 : MonoBehaviour
{
    public AuidoPlayBase audioPlay;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是数据带来的多态行为
        //某些行为的变化是因为数据的不同带来的
        //我们可以利用面向对象的特性和原则，以及设计模式相关知识点
        //结合ScriptableObject做出更加方便的功能

        //比如随机音效，物品拾取，AI等等等

        //随机音效（里氏替换原则和依赖倒转原则）
        //播放音乐时，可能会随机播放多个音效当中的一种

        //物品拾取（里氏替换原则和依赖倒转原则）
        //比如拾取一个物品，物品给玩家带来不同的效果

        //AI
        //不同数据带来的不同行为模式

        //为了方便我们使用，我们可以利用ScriptableObject的可配置性来制作这些功能
        #endregion

        #region 知识点二 举例说明

        #endregion

        audioPlay.Play(this.GetComponent<AudioSource>());
        #region 总结
        //其实这些例子中的功能就算不用ScriptableObject也是能够用
        //面向对象的思想 结合配置文件来完成的
        //但是ScriptableObject具备自己的几个优点
        //1.更方便的配置
        //2.共享数据节约内存
        //在实现某些功能的时候，使用ScriptableObject会更加方便我们的使用
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
