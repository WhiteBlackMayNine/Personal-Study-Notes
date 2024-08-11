using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager 
{
    private static DataManager instance = new DataManager();
    public static DataManager Instance => instance;

    private InputInfo inputInfo;

    public InputInfo InputInfo => inputInfo;

    //ר�Ŵ洢 json ����
    private string jsonStr;

    private DataManager()
    {
        inputInfo = new InputInfo();

        //�õ��˴洢������Ϣ���ַ���
        //�������� �ַ���API �����滻
        jsonStr = Resources.Load<TextAsset>("Lesson24").text;
    }

    //�����滻
    public InputActionAsset GetActionAsset()
    {
        string str = jsonStr.Replace("<up>", inputInfo.up);
        str = str.Replace("<down>", inputInfo.down);
        str = str.Replace("<left>", inputInfo.left);
        str = str.Replace("<right>", inputInfo.right);
        str = str.Replace("<fire>", inputInfo.fire);
        str = str.Replace("<jump>", inputInfo.jump);

        return InputActionAsset.FromJson(str); 
    }

}
