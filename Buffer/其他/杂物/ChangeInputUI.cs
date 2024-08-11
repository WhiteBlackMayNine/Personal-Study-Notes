using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;


//通过枚举的形式去记录按键

public enum BTN_TYPE
{
    UP, 
    DOWN,
    LEFT,
    RIGHT,

    FIRE,
    JUMP

}


public class ChangeInputUI : MonoBehaviour
{
    public Text txtUp;
    public Text txtDown;
    public Text txtLeft;
    public Text txtRight;

    public Text txtFire;
    public Text txtJump;

    public Button btnUp; 
    public Button btnDown;
    public Button btnLeft;
    public Button btnRight;

    public Button btnFire;
    public Button btnJump;


    private InputInfo inputInfo;

    //记录当前改哪一个键
    private BTN_TYPE nowType;
    // Start is called before the first frame update
    void Start()
    {
        //这里初始化，得到的就是默认按键
        inputInfo = new InputInfo();
        UpdateBtnInfo();

        #region 按钮的监听

        btnUp.onClick.AddListener(() =>
        {
            ChangeBtn(BTN_TYPE.UP);
        });

        btnDown.onClick.AddListener(() =>
        {
            ChangeBtn(BTN_TYPE.DOWN);
        });

        btnLeft.onClick.AddListener(() =>
        {
            ChangeBtn(BTN_TYPE.LEFT);
        });

        btnRight.onClick.AddListener(() =>
        {
            ChangeBtn(BTN_TYPE.RIGHT);
        });

        btnFire.onClick.AddListener(() =>
        {
            ChangeBtn(BTN_TYPE.FIRE);
        });

        btnJump.onClick.AddListener(() =>
        {
            ChangeBtn(BTN_TYPE.JUMP);
        });

        #endregion

    }

    private void ChangeBtn(BTN_TYPE type)
    {
        nowType = type;

        InputSystem.onAnyButtonPress.CallOnce(ChangeBtenReally);
    }

    //在这个函数中 去进行真正的按键修改
    private void ChangeBtenReally(InputControl control)
    {
        // control.path 得到的 /Keyboard/y
        //由于 path 路径于 Json 文件格式不同，需要先进行一些修改
        //由于第一个 / 左边为空，所以索引为0的字符串为 Null

        string[] str = control.path.Split('/');
        string path = "<" + str[1] + ">/" + str[2];
        switch (nowType)
        {
            case BTN_TYPE.UP:
                inputInfo.up = path;
                break; 
            case BTN_TYPE.DOWN:
                inputInfo.down = path;
                break; 
            case BTN_TYPE.LEFT:
                inputInfo.left = path;
                break;
            case BTN_TYPE.RIGHT:
                inputInfo.right = path;
                break;
            case BTN_TYPE.FIRE:
                inputInfo.fire = path;
                break;
            case BTN_TYPE.JUMP:
                inputInfo.jump = path;
                 break;
        }
        UpdateBtnInfo();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    //这里更新 UI 信息
    private void UpdateBtnInfo()
    {
        txtUp.text = inputInfo.up;
        txtDown.text = inputInfo.down;
        txtLeft.text = inputInfo.left;
        txtRight.text = inputInfo.right;

        txtFire.text = inputInfo.fire;
        txtJump.text = inputInfo.jump;
    }
}
