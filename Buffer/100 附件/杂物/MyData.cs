using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName ="MrTangData", menuName ="ScriptableObject/�ҵ�����", order = 0)]
public class MyData : ScriptableObject
{
    //������Աʱ��Ҫע��
    //���ǿ��������κ����͵ĳ�Ա����
    //������Ҫע�⣺���ϣ��֮����Inspector�������ܹ��༭��
    //���������������ı������� Ҫ�� MonoBehavior����public�����Ĺ�����һ����

    public int i;
    public float f;
    public bool b;

    public GameObject obj;
    public Material m;
    public AudioClip audioClip;
    public VideoClip videoClip;

    private void Awake()
    {
        Debug.Log("�����ļ�����ʱ�����");
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
