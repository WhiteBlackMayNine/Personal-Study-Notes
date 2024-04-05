using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：协程（协同程序）
//***************************************** 
public class No15_Coroutine : MonoBehaviour
{
    //用法和用途（1.延时调用，2.和其他逻辑一起协同执行
    //(比如一些很耗时的工作，在这个协程中执行异步操作，比如下载文件，加载文件等)

    public Animator animator;
    public int grisCount;
    private int grisNum;

    void Start()
    {
        //协程的启动
        //StartCoroutine("ChangeState");
        //StartCoroutine(ChangeState());
        //IEnumerator ie = ChangeState();
        //StartCoroutine(ie);
        //协程的停止
        //StopCoroutine("ChangeState");
        //无法停止协程
        //StopCoroutine(ChangeState());

        //StopCoroutine(ie);

        //StopAllCoroutines();


        StartCoroutine("CreateGris");
    }

    void Update()
    {
        
    }

    IEnumerator ChangeState()
    {
        //暂停几秒（协程挂起）
        yield return new WaitForSeconds(2);
        animator.Play("Walk");
        yield return new WaitForSeconds(3);
        animator.Play("Run");
        //等待一帧 yield return n(n是任意数字)
        yield return null;
        yield return 100000;
        print("转换成Run状态了");
        //在本帧帧末执行以下逻辑
        yield return new WaitForEndOfFrame();
    }

    IEnumerator CreateGris()
    {
        StartCoroutine(SetCreateCount(5));
        while (true)
        {
            if (grisNum>=grisCount)
            {
                yield break;
            }
            Instantiate(animator.gameObject);
            grisNum++;
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator SetCreateCount(int num)
    {
        grisCount =num;
        yield return null;
    }
}
