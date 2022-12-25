using UnityEngine;
using System.Collections;

[AddComponentMenu("Game/AutoDestroy")]
public class AutoDestroy : MonoBehaviour {

    public float m_timer = 1.0f;

	void Start () {
        // 这里可采用对象池的方式避免在游戏运行中使用Instantiate和Destroy
        Destroy(this.gameObject, m_timer);
	}
}
