using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLesson24 : MonoBehaviour
{
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {   
        //��ȡ���
        playerInput = this.GetComponent<PlayerInput>();
        //���� InputActionAsset �ļ�
        playerInput.actions = DataManager.Instance.GetActionAsset();
        //����
        playerInput.actions.Enable();
        //�����߼�
        playerInput.onActionTriggered += (context) =>
        {
            
        };

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //����Ҳ����ļ�Ч��
    public void ChangeInput()
    {
        //�ļ� ���� �ı� ���� PlayerInput �Ϲ���������������Ϣ

        //���� InputActionAsset �ļ�
        playerInput.actions = DataManager.Instance.GetActionAsset();
        //����
        playerInput.actions.Enable();
    }
}
