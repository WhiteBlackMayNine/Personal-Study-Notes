using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;


//ͨ��ö�ٵ���ʽȥ��¼����

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

    //��¼��ǰ����һ����
    private BTN_TYPE nowType;
    // Start is called before the first frame update
    void Start()
    {
        //�����ʼ�����õ��ľ���Ĭ�ϰ���
        inputInfo = new InputInfo();
        UpdateBtnInfo();

        #region ��ť�ļ���

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

    //����������� ȥ���������İ����޸�
    private void ChangeBtenReally(InputControl control)
    {
        // control.path �õ��� /Keyboard/y
        //���� path ·���� Json �ļ���ʽ��ͬ����Ҫ�Ƚ���һЩ�޸�
        //���ڵ�һ�� / ���Ϊ�գ���������Ϊ0���ַ���Ϊ Null

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

    //������� UI ��Ϣ
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
