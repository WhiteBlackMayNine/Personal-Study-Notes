using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson6 : MonoBehaviour
{
    public BulletInfo info;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 使用预设体对象可能存在的内存浪费问题
        //对于只用不变的数据
        //以面向对象的思想去声明对象类是可能存在内存浪费的问题的

        //我们以子弹对象为例
        #endregion

        #region 知识点二 举例说明 利用ScriptableObject数据对象 更加节约内存

        #endregion

        #region 总结
        //对于不同对象，使用相同数据时
        //我们可以使用ScriptableObject来节约内存
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            info.speed += 1;
    }
}
