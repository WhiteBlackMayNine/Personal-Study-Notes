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
        //获取组件
        playerInput = this.GetComponent<PlayerInput>();
        //关联 InputActionAsset 文件
        playerInput.actions = DataManager.Instance.GetActionAsset();
        //启用
        playerInput.actions.Enable();
        //处理逻辑
        playerInput.onActionTriggered += (context) =>
        {
            
        };

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //让玩家产生改键效果
    public void ChangeInput()
    {
        //改键 就是 改变 我们 PlayerInput 上关联的输入配置信息

        //关联 InputActionAsset 文件
        playerInput.actions = DataManager.Instance.GetActionAsset();
        //启用
        playerInput.actions.Enable();
    }
}
