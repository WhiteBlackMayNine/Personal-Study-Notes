using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectTool
{
    //Unity �Ϸ��Ĳ˵��������
    //�ڲ˵�������󣬾ͻ�ִ�� CreateMyData() ����
    [MenuItem("ScriptableObject/CreateMyData")]
    public static void CreateMyData()
    {
        //��д����������Դ�ļ��Ĵ���
        MyData asset = ScriptableObject.CreateInstance<MyData>();

        //ͨ���༭��API �������ݴ���һ��������Դ�ļ�
        AssetDatabase.CreateAsset(asset, "Assets/Resources/MyDataTest.asset");
        //���洴������Դ
        AssetDatabase.SaveAssets();
        //ˢ�½���
        AssetDatabase.Refresh();
    }
}
