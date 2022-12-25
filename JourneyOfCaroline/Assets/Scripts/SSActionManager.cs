using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//动作管理基类
public class SSActionManager : MonoBehaviour
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();    //动作字典
    private List<int> waitingDelete = new List<int>();                              //等待删除的key列表   
    private List<SSAction> waitingAdd = new List<SSAction>();                       //等待执行的动作列表             

    protected void Update()
    {
        //将等待执行的动作加入字典并清空待执行列表
        foreach (SSAction ac in waitingAdd)
        {
            actions[ac.GetInstanceID()] = ac;
        }
        waitingAdd.Clear();

        //遍历字典中的动作，分执行/删除
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ssac = kv.Value;
            if (ssac.enable)
            {
                ssac.Update();
            }
            else if (ssac.destroy)
            {
                waitingDelete.Add(ssac.GetInstanceID());
            }
        }

        //删除所有已完成的动作并清空待删除列表
        foreach (int key in waitingDelete)
        {
            SSAction ssac = actions[key];
            actions.Remove(key);
            Object.Destroy(ssac);
        }
        waitingDelete.Clear();
    }

    //外界只需要调用动作管理类的RunAction函数即可完成动作。
    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
    {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }
}
