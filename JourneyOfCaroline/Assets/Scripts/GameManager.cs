using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[AddComponentMenu("Game/GameManager")]
public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    // ��Ϸ�÷�
    public int m_score = 0;

    // ��Ϸ��ߵ÷�
    public static int m_hiscore = 0;

    // ��ҩ����
    public int m_ammo = 100;

    // ��Ϸ����
    Player m_player;

    // UI����
    Text txt_ammo;
    Text txt_hiscore;
    Text txt_life;
    Text txt_score;
    Button button_restart;
	// Use this for initialization
	void Start () {

        Instance = this;

        // �������
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // ���UI����
        GameObject uicanvas = GameObject.Find("Canvas");
        foreach (Transform t in uicanvas.transform.GetComponentsInChildren<Transform>())
        {

            if (t.name.CompareTo("txt_ammo") == 0)
            {
                txt_ammo = t.GetComponent<Text>();
            }
            else if (t.name.CompareTo("txt_hiscore") == 0)
            {
                txt_hiscore = t.GetComponent<Text>();
                txt_hiscore.text = "High Score " + m_hiscore;
            }
            else if (t.name.CompareTo("txt_life") == 0)
            {
                txt_life = t.GetComponent<Text>();
            }
            else if (t.name.CompareTo("txt_score") == 0)
            {
                txt_score = t.GetComponent<Text>();
            }
            else if (t.name.CompareTo("Restart Button") == 0)
            {
                button_restart = t.GetComponent<Button>();
                button_restart.onClick.AddListener(delegate (){//�������¿�ʼ��Ϸ��ť�¼�
                    // ��ȡ��ǰ�ؿ�
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                });
                button_restart.gameObject.SetActive(false);  // ��Ϸ�����������¿�ʼ��Ϸ��ť
            }
        }

       
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    //void OnGUI()
    //{
    //    if (m_player.m_life <= 0)
    //    {
    //        // ������ʾ����
    //        GUI.skin.label.alignment = TextAnchor.MiddleCenter;

    //        // �ı����ִ�С
    //        GUI.skin.label.fontSize = 40;

    //        // ��ʾGame Over
    //        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Game Over");

    //        // ��ʾ������Ϸ��ť
    //         GUI.skin.label.fontSize = 30;
    //        if ( GUI.Button( new Rect( Screen.width*0.5f-150,Screen.height*0.75f,300,40),"Try again"))
    //        {
    //            Application.LoadLevel(Application.loadedLevelName);
    //        }
    //    }
    //}

    // ���·���
    public void SetScore(int score)
    {
        m_score+= score;

        if (m_score > m_hiscore)
            m_hiscore = m_score;

        txt_score.text = "Score <color=yellow>" + m_score  + "</color>";;
        txt_hiscore.text = "High Score " + m_hiscore;
      
    }

    // ���µ�ҩ
    public void SetAmmo(int ammo)
    {
        m_ammo -= ammo;

        // �����ҩΪ�����������
        if (m_ammo <= 0)
            m_ammo = 100 - m_ammo;
        txt_ammo.text = m_ammo.ToString()+"/100";
    }

    // ��������
    public void SetLife(int life)
    {
        txt_life.text = life.ToString();
        if ( life<=0)  // ����������Ϊ0ʱ��ʾ���¿�ʼ��Ϸ��ť
            button_restart.gameObject.SetActive(true);
    }



}