using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPoolItem
{
    void Spawn();
    void Recycl();
}
public abstract class PoolItemBase : MonoBehaviour, IPoolItem
{
    private void OnEnable()
    {
        Spawn();
    }

    private void OnDisable()
    {
        Recycl();
    }

    public virtual void Recycl()
    {

    }

    public virtual void Spawn()
    {

    }
}
