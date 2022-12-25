using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    public GameObject disk_prefab;

    private List<DiskData> use;    //已使用的飞碟
    private List<DiskData> free;    //空闲的飞碟

    void Start()
    {
        use = new List<DiskData>();
        free = new List<DiskData>();
        disk_prefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UFO"), Vector3.zero, Quaternion.identity);
        disk_prefab.SetActive(false);
    }

    //获取飞碟
    public GameObject GetDisk(int round)
    {
        GameObject disk;

        //如果有空闲的飞碟，则直接使用。否则生成新的飞碟
        if (free.Count > 0)
        {
            disk = free[0].gameObject;
            free.Remove(free[0]);
        }
        else
        {
            disk = GameObject.Instantiate<GameObject>(disk_prefab, Vector3.zero, Quaternion.identity);
            disk.AddComponent<DiskData>();
        }
        disk.gameObject.name = "Disk";

        //生成飞碟的规则
        float base_speed = 3 + round * 0.8f;        //round越大，速度基数越大，难度越高
        Vector3 base_scale = new Vector3(4.0f, 0.5f, 4.0f);    //颜色和大小有关
        float rand_y = Random.Range(-0.5f, 0.5f);   //垂直方向速度随机生成
        int side = Random.Range(0, 2);               //水平速度方向
        if (side == 0)
        {
            side = -1;
        }
        int rand_num = Random.Range(1, 4);         //颜色和速度有关
        int[] point_str = { 1, 2, 3 };
        float[] speed_str = { 0.6f, 1.2f, 1.8f };
        float[] localScale_str = { 1.2f, 1f, 0.8f };

        disk.GetComponent<DiskData>().points = point_str[rand_num - 1];
        disk.GetComponent<DiskData>().speed = speed_str[rand_num - 1] * base_speed;
        disk.GetComponent<Transform>().localScale = localScale_str[rand_num - 1] * base_scale;
        disk.GetComponent<DiskData>().direction = new Vector3(side, rand_y, 0);
        if (rand_num == 1)
            disk.GetComponent<Renderer>().material.color = Color.red;
        else if (rand_num == 2)
            disk.GetComponent<Renderer>().material.color = Color.black;
        else
            disk.GetComponent<Renderer>().material.color = Color.white;

        use.Add(disk.GetComponent<DiskData>());

        return disk;
    }

    //释放
    public void FreeDisk(GameObject disk)
    {
        foreach (DiskData d in use)
        {
            if (d.gameObject.GetInstanceID() == disk.GetInstanceID())
            {
                disk.SetActive(false);  //先灭活
                use.Remove(d);
                free.Add(d);            //重新放回到对象池中
                break;
            }
        }
    }

    //销毁所有对象
    public void Clear()
    {
        foreach (DiskData disk in free)
        {
            disk.gameObject.transform.position = new Vector3(0, -20, 0);
            Destroy(disk.gameObject);
        }
        free.Clear();
        foreach (DiskData disk in use)
        {
            //让FlyAction结束并回调，再销毁对象。
            disk.gameObject.transform.position = new Vector3(0, -20, 0);
            Destroy(disk.gameObject);
        }
        use.Clear();
    }
}
