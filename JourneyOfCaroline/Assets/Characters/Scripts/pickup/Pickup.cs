using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickup : MonoBehaviour
{
    int RedFlowerCount = 0;
    int MushroomCount = 0;
    public AudioSource pickup; // 拾取物品时触发音效
    private float delayTime = 0.5f;

    private void jumpScene()
    {
        SceneManager.LoadScene("UFO World");
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Flower"))
        {
            Destroy(collision.gameObject);
            RedFlowerCount += 1;
            pickup.Play();
        } 
        else if(collision.CompareTag("Mushroom"))
        {
            Destroy(collision.gameObject);
            MushroomCount += 1;
            pickup.Play();
        }
        if(RedFlowerCount >= 5 && MushroomCount >= 5)
        {
            Invoke("jumpScene", delayTime);
        }

    }
    private void OnGUI()
    {
        GUI.color = Color.white;
        GUI.skin.label.fontSize = 40;
        GUI.Label(new Rect(100, 600, 500, 900), "RedFlower Num: " + RedFlowerCount);
        GUI.Label(new Rect(100, 700, 500, 900), "Mushroom Num: " + MushroomCount);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
