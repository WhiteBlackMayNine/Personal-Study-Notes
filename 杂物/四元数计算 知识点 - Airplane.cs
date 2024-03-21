using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_FireType
{
    //单发
    One,
    //双发
    Two,
    //扇形
    Three,
    //环形
    Round
}

public class Airplane : MonoBehaviour
{
    private E_FireType nowType = E_FireType.One;

    public GameObject bullet;

    public int roundNum = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Alpha1) )
        {
            nowType = E_FireType.One;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nowType = E_FireType.Two;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            nowType = E_FireType.Three;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            nowType = E_FireType.Round;
        }

        if( Input.GetKeyDown(KeyCode.Space) )
        {
            Fire();
        }
    }

    private void Fire()
    {
        switch (nowType)
        {
            case E_FireType.One:
                Instantiate(bullet, this.transform.position, this.transform.rotation);
                break;
            case E_FireType.Two:
                Instantiate(bullet, this.transform.position - this.transform.right * 0.5f, this.transform.rotation);
                Instantiate(bullet, this.transform.position + this.transform.right * 0.5f, this.transform.rotation);
                break;
            case E_FireType.Three:
                //朝自己的面朝向发射
                Instantiate(bullet, this.transform.position, this.transform.rotation);
                //朝自己左侧旋转20度再发射——知识点 四元数*四元数=一个新的四元数 相当于是旋转量的叠加
                Instantiate(bullet, this.transform.position, this.transform.rotation * Quaternion.AngleAxis(-20, Vector3.up));
                //朝自己左侧旋转20度再发射——知识点 四元数*四元数=一个新的四元数 相当于是旋转量的叠加
                Instantiate(bullet, this.transform.position, this.transform.rotation * Quaternion.AngleAxis(20, Vector3.up));
                break;
            case E_FireType.Round:
                float angle = 360 / roundNum;
                for (int i = 0; i < roundNum; i++)
                    Instantiate(bullet, this.transform.position, this.transform.rotation * Quaternion.AngleAxis(i*angle, Vector3.up));
                break;
        }
    }
}
