using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson22_Exercises : MonoBehaviour
{
    //移动速度
    public float moveSpeed;
    //旋转速度
    public float roundSpeed;

    // Start is called before the first frame update
    void Start()
    {
        #region 练习题
        //世界坐标原点有一个立方体，键盘WASD键可以控制其前后移动和旋转
        //请结合所学知识实现
        //1.按J键在立方体面朝向前方1米处进行立方体范围检测
        //2.按K键在立方体前面5米范围内进行胶囊范围检测
        //3.按L键以立方体脚下为原点，半径10米内进行球形范围检测
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        //位移
        this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * Input.GetAxis("Vertical"));
        //旋转
        this.transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * roundSpeed * Time.deltaTime);

        if( Input.GetKeyDown(KeyCode.J) )
        {
            Collider[] colliders = Physics.OverlapBox( this.transform.position + this.transform.forward, 
                                                       Vector3.one*0.5f, //长宽高的一半
                                                       this.transform.rotation, 
                                                       1 << LayerMask.NameToLayer("Monster"));
            for (int i = 0; i < colliders.Length; i++)
            {
                print("物体受伤" + colliders[i].gameObject.name);
            }
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            Collider[] colliders = Physics.OverlapCapsule(this.transform.position,
                                                         this.transform.position + this.transform.forward * 5,
                                                         0.5f,
                                                         1 << LayerMask.NameToLayer("Monster"));
            for (int i = 0; i < colliders.Length; i++)
            {
                print("物体受伤" + colliders[i].gameObject.name);
            }
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 10, 1 << LayerMask.NameToLayer("Monster"));
            for (int i = 0; i < colliders.Length; i++)
            {
                print("物体受伤" + colliders[i].gameObject.name);
            }
        }
    }
}
