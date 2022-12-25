using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class toFirstScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeScene()/*定义一个切换场景*/
    {
        SceneManager.LoadScene("Kingdom_of_Sky_Mirrors");
    }
}
