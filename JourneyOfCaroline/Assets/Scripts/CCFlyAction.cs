using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCFlyAction : SSAction
{
    float speed;
    Vector3 direction;

    public static CCFlyAction GetSSAction(Vector3 dir, float speed)
    {
        CCFlyAction tmp = ScriptableObject.CreateInstance<CCFlyAction>();
        tmp.speed = speed;
        tmp.direction = dir;
        return tmp;
    }

    public override void Start()
    {
        gameobject.GetComponent<Rigidbody>().isKinematic = false;
        //为物体增加水平初速度，否则会由于受到重力严重影响运动状态。
        gameobject.GetComponent<Rigidbody>().velocity = speed * direction;
    }

    public override void Update()
    {
        //动作运行
        transform.Translate(direction * speed * Time.deltaTime);
        //判断飞碟是否落地 
        if (this.transform.position.y < -10)
        {
            this.destroy = true;
            this.enable = false;
            this.callback.SSActionEvent(this);
        }
    }
}