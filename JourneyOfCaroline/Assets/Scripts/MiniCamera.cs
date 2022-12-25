using UnityEngine;
using System.Collections;

[AddComponentMenu("Game/MiniCamera")]
public class MiniCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // 获得屏幕分辨率比例
        float ratio = (float)Screen.width / (float)Screen.height;
        // 使摄像机视图永远是一个正方向, rect的前两个参数表示XY位置，后两个参数是XY大小
        this.GetComponent<Camera>().rect = new Rect((1 - 0.2f), (1 - 0.2f * ratio), 0.2f, 0.2f * ratio);
	}
}
