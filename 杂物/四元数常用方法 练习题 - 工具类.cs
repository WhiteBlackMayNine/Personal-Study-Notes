using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    public static void MyLookAt(this Transform obj, Transform target)
    {
        Vector3 vec = target.position - obj.position;
        obj.transform.rotation = Quaternion.LookRotation(vec);
    }
}
