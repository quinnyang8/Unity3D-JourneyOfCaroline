using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//动作基类
public class SSAction : ScriptableObject
{
    public bool enable = true;                      //是否可进行
    public bool destroy = false;                    //是否已完成

    public GameObject gameobject;                   //动作对象
    public Transform transform;                     //动作对象的transform
    public ISSActionCallback callback;              //回调函数

    protected SSAction() { }

    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}
