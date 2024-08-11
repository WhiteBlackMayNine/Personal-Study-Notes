using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//*****************************************
//创建人： Trigger 
//功能说明：场景管理
//***************************************** 
public class No21_SceneManager : MonoBehaviour
{
    private AsyncOperation ao;
    void Start()
    {
        //SceneManager.LoadScene(1);
        //SceneManager.LoadScene("TriggerTest");
        //SceneManager.LoadScene(2);
        //SceneManager.LoadSceneAsync(2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SceneManager.LoadSceneAsync(2);
            StartCoroutine(LoadNextAsyncScene());
        }
        if (Input.anyKeyDown&&ao.progress>=0.9f)
        {
            ao.allowSceneActivation = true;
        }
    }

    IEnumerator LoadNextAsyncScene()
    {
        ao= SceneManager.LoadSceneAsync(2);
        ao.allowSceneActivation = false;
        while (ao.progress<0.9f)
        {
            //当前场景加载进度小于0.9
            //当前场景挂起，一直加载，直到加载基本完成
            yield return null;
        }
        Debug.Log("按下任意键继续游戏");
    }
}
