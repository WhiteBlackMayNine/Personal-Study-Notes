using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr
{
    private static SceneMgr instance = new SceneMgr();

    public static SceneMgr Instance => instance;

    private SceneMgr() { }

    public void LoadScene(string name, UnityAction action)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        ao.completed += (a) =>
        {
            //通过那么大表达式 包裹一层
            //在内部 直接调用外部传入的委托即可
            action();
        };
    }
}
