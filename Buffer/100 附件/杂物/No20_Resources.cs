using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：资源加载
//***************************************** 
public class No20_Resources : MonoBehaviour
{
    void Start()
    {
        //Debug.Log(Resources.Load<AudioClip>("sound"));
        //AudioClip audioClip = Resources.Load<AudioClip>("sound");
        //AudioSource.PlayClipAtPoint(audioClip,transform.position);
        //AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("sound"), transform.position);
        //Instantiate(Resources.Load<GameObject>(@"Prefabs/GrisCollierTest"));

        Object obj= Resources.Load("sound");
        //AudioClip ac = obj as AudioClip;
        AudioClip ac = (AudioClip)obj;
        AudioSource.PlayClipAtPoint(ac, transform.position);
        
        //Resources.LoadAll<AudioClip>("Prefabs");
        AudioClip[] audioClips= Resources.LoadAll<AudioClip>("");
        foreach (var item in audioClips)
        {
            Debug.Log(item);
        }

        //Resources.UnloadAsset
    }

    void Update()
    {

    } 
}
