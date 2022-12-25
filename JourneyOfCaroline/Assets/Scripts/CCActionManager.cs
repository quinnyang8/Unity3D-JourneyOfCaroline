using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback, IActionManager
{
    public CCFlyAction Action;
    public FirstController controller;
    public void Start()
    {
        controller = (FirstController)SSDirector.GetInstance().CurrentScenceController;
    }

    public void Fly(GameObject disk, float speed, Vector3 direction)
    {
        Action = CCFlyAction.GetSSAction(direction, speed);
        RunAction(disk, Action, this);
    }

    //回调函数
    public void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Competed,
        int intParam = 0,
        string strParam = null,
        Object objectParam = null)
    {
        controller.diskFactory.FreeDisk(source.gameobject);//释放资源
    }
}
