using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UserGUI : MonoBehaviour
{

    private IUserAction userAction;     //GUI回调接口

    public int score;   //得分
    public int round;   //回合
    public int status;  //0准备，1进行，2结束
    GUIStyle Style1;
    GUIStyle Style2;
    GUIStyle fontStyle = new GUIStyle();
    public Texture buttonTexture;
    void Start()
    {
        score = 0;
        status = 0;
        round = 0;

        Style1 = new GUIStyle();
        Style1.normal.textColor = Color.black;
        Style1.fontSize = 30;

        Style2 = new GUIStyle();
        Style2.normal.textColor = Color.black;
        Style2.fontSize = 30;


 
        fontStyle.alignment = TextAnchor.MiddleCenter;
        fontStyle.fontSize = 45;
        fontStyle.normal.textColor = Color.black;
        fontStyle.normal.background = (Texture2D)buttonTexture;


        userAction = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    }

    void OnGUI()
    {
        if (status == 0)
        {
            GUI.Label(new Rect(240, 30, 50, 50), "Let's shoot the UFO!", Style2);

            if (GUI.Button(new Rect(280, 180, 50, 90), "Start", fontStyle))
            {
                status = 1;
                userAction.Reset();
            }
        }

        if (status == 1)
        {
            GUI.Label(new Rect(20, 20, 100, 50), "Score  " + score, Style1);
            GUI.Label(new Rect(20, 80, 100, 50), "Round  " + round, Style1);

            if (Input.GetButtonDown("Fire1"))
            {
                userAction.Click(Input.mousePosition);
            }
            if (GUI.Button(new Rect(20, 160, 100, 40), "ReStart"))
            {
                status = 0;
                userAction.Reset();
            }
        }

        if (status == 2)
        {
            GUI.Label(new Rect(280, 100, 50, 40), "Game Over", Style1);
            GUI.Label(new Rect(280, 200, 50, 40), "Score:  " + score, Style1);
            if (GUI.Button(new Rect(300, 330, 100, 40), "ReStart"))
            {
                status = 0;
                userAction.Reset();
            }
        }
    }
}
