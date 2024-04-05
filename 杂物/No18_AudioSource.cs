using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：播放音频的类
//***************************************** 
public class No18_AudioSource : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip musicClip;
    public AudioClip soundClip;
    private bool muteState;
    private bool pauseState;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = musicClip;
        //audioSource.Play();
        audioSource.volume = 1;
        audioSource.time = 3;
    }

    void Update()
    {
        //静音
        if (Input.GetKeyDown(KeyCode.M))
        {
            muteState = !muteState;
            audioSource.mute = muteState;
        }
        //暂停
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseState = !pauseState;
            if (pauseState)
            {
                audioSource.Pause();
            }
            else
            {
                audioSource.UnPause();
            }
        }
        //停止
        if (Input.GetKeyDown(KeyCode.S))
        {
            audioSource.Stop();
        }
        //播放一次
        if (Input.GetKeyDown(KeyCode.K))
        {
            //audioSource.PlayOneShot(soundClip);
            AudioSource.PlayClipAtPoint(soundClip,transform.position);
        }
    } 
}
