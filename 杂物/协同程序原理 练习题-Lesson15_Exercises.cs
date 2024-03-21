using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson15_Exercises : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Unity自带的协程协调器 开启协程函数（迭代器函数）
        //StartCoroutine(MyTest());

        CoroutineMgr.Instance.MyStartCoroutine(MyTest());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MyTest()
    {
        print("1");
        yield return 1;//如果是用自带的 开启 那么数字代表等待1帧继续执行之后的内容
        print("2");
        yield return 2;
        print("3");
        yield return 3;
        print("4");
    }
}
