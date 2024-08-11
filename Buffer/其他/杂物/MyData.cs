using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName ="MrTangData", menuName ="ScriptableObject/我的数据", order = 0)]
public class MyData : ScriptableObject
{
    //声明成员时需要注意
    //我们可以声明任何类型的成员变量
    //但是需要注意：如果希望之后在Inspector窗口中能够编辑它
    //那你在这里声明的变量规则 要和 MonoBehavior当中public变量的规则是一样的

    public int i;
    public float f;
    public bool b;

    public GameObject obj;
    public Material m;
    public AudioClip audioClip;
    public VideoClip videoClip;

    private void Awake()
    {
        Debug.Log("数据文件创建时会调用");
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
        
    }


    private void OnValidate()
    {
        //Debug.Log("123");
    }

    public void PrintInfo()
    {
        Debug.Log(i);
        Debug.Log(f);
        Debug.Log(b);
    }

}
