using UnityEngine;
using System.Collections;

[AddComponentMenu("Game/AutoDestroy")]
public class AutoDestroy : MonoBehaviour {

    public float m_timer = 1.0f;

	void Start () {
        // ����ɲ��ö���صķ�ʽ��������Ϸ������ʹ��Instantiate��Destroy
        Destroy(this.gameObject, m_timer);
	}
}
