using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCube : MonoBehaviour
{
    public Transform B;
    public float moveSpeed;
    
    private Vector3 pos;
    private Vector3 bNowPos;
    private Vector3 startPos;
    private float time;
    // Update is called once per frame
    void Update()
    {
        //第一种 就是 先快后慢的形式
        //pos = this.transform.position;

        //pos.x = Mathf.Lerp(pos.x, B.position.x, Time.deltaTime * moveSpeed);
        //pos.y = Mathf.Lerp(pos.y, B.position.y, Time.deltaTime * moveSpeed);
        //pos.z = Mathf.Lerp(pos.z, B.position.z, Time.deltaTime * moveSpeed);

        //this.transform.position = pos;
        
        //第二种 就是 匀速运动
        if( bNowPos != B.transform.position)
        {
            time = 0;
            bNowPos = B.transform.position;
            startPos = this.transform.position;
        }

        time += Time.deltaTime;
        pos.x = Mathf.Lerp(startPos.x, bNowPos.x, time);
        pos.y = Mathf.Lerp(startPos.y, bNowPos.y, time);
        pos.z = Mathf.Lerp(startPos.z, bNowPos.z, time);
        this.transform.position = pos;
    }
}
