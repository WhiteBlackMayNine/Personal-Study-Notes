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

    //专门存储 json 数据
    private string jsonStr;

    private DataManager()
    {
        inputInfo = new InputInfo();

        //得到了存储输入信息的字符串
        //可以利用 字符串API 进行替换
        jsonStr = Resources.Load<TextAsset>("Lesson24").text;
    }

    //进行替换
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
