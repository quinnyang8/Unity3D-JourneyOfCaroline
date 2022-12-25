using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    public DiskFactory diskFactory;
    public IActionManager actionManager;
    public ScoreRecorder scoreRecorder;
    public UserGUI userGUI;
    int round;          //回合
    float curr_time;     //当前时间，用于update的累计计算
    int curr_disk_num;       //该回合飞碟数量
    public AudioSource crack;    // 飞碟破裂音
    private float delayTime = 0.5f;

    private void jumpScene()
    {
        SceneManager.LoadScene("Chaotic Witches World");
    }

    // Start is called before the first frame update
    void Start()
    {
        //添加导演
        SSDirector.GetInstance().CurrentScenceController = this;

        //添加脚本，初始化对象
        gameObject.AddComponent<DiskFactory>();
        gameObject.AddComponent<CCActionManager>();
        userGUI = gameObject.AddComponent<UserGUI>();
        diskFactory = Singleton<DiskFactory>.Instance;
        actionManager = Singleton<CCActionManager>.Instance;

        //加载其他资源
        LoadResources();
    }

    public void LoadResources()
    {
        scoreRecorder = new ScoreRecorder();
        scoreRecorder.Reset();
        round = 0;
        curr_time = 0;
        curr_disk_num = 0;
    }

    //发射飞碟
    public void SendDisk()
    {
        GameObject disk = diskFactory.GetDisk(round);
        float side = -disk.GetComponent<DiskData>().direction.x;        //与速度水平方向相反
        disk.transform.position = new Vector3(side * 15f, UnityEngine.Random.Range(0f, 8f), 10);
        disk.SetActive(true);         //激活

        actionManager.Fly(disk, disk.GetComponent<DiskData>().speed, disk.GetComponent<DiskData>().direction);//设置飞行动作
    }

    //击中飞碟
    public void Click(Vector3 position)
    {
        Camera camera = Camera.main;
        Ray ray = camera.ScreenPointToRay(position);
        RaycastHit[] Hit = Physics.RaycastAll(ray);

        for (int i = 0; i < Hit.Length; i++)
        {
            if (Hit[i].collider.gameObject.GetComponent<DiskData>() != null)
            {
                GameObject disk = Hit[i].collider.gameObject;
                disk.transform.position = new Vector3(10, -20, 0);
                diskFactory.FreeDisk(disk);
                scoreRecorder.Record(disk);
                userGUI.score = scoreRecorder.score;
                crack.Play();

            }
        }
    }

    //清除界面中的飞碟
    public void ClearDisk()
    {
        GameObject[] obj = FindObjectsOfType(typeof(GameObject)) as GameObject[]; //获取所有gameobject元素
        foreach (GameObject child in obj)
        {
            if (child.gameObject.name == "Disk")
            {
                child.gameObject.SetActive(false);
            }
        }
        diskFactory.Clear();        //彻底销毁
    }

    public void Reset()
    {
        round = 0;
        curr_time = 0;
        curr_disk_num = 0;
        userGUI.round = round;
        scoreRecorder.Reset();
        userGUI.score = scoreRecorder.score;
        ClearDisk();
    }

    void Update()
    {
        if (userGUI.status == 1)
        {
            if (round <= 10)    //游戏未结束
            {
                curr_time += Time.deltaTime;
                if (curr_time > 2)          //每2秒生成一次
                {
                    curr_time = 0;
                    //游戏初始化
                    if (round == 0)
                    {
                        round++;
                        userGUI.round = round;
                    }
                    int rand_disk = Random.Range(3, 8);     //随机发射3-7个飞碟
                    for (int i = 0; i < rand_disk; i++)
                    {
                        SendDisk();
                        curr_disk_num++;
                        if (curr_disk_num == 10)
                        {
                            break;
                        }
                    }
                    if (curr_disk_num == 10 && round <= 10)     //发射10个后更新轮次
                    {
                        curr_disk_num = 0;
                        round++;
                        userGUI.round = round;
                    }
                }
            }
            if(scoreRecorder.score >= 35)
            {
                Invoke("jumpScene", delayTime);
            }
            if (round == 11)     //结束游戏
            {
                userGUI.round = 10;
                curr_time += Time.deltaTime;
                if (curr_time > 5)
                {
                    userGUI.status = 2;
                }
            }
        }
    }
}
