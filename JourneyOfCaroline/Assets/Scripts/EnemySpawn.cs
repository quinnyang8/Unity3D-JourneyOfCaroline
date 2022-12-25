using UnityEngine;
using System.Collections;

[AddComponentMenu("Game/EnemySpawn")]
public class EnemySpawn : MonoBehaviour
{
    // ���˵�Prefab
    public Transform m_enemy;

    //���ɵĵ�������
    public int m_enemyCount = 0;

    // ���˵������������
    public int m_maxEnemy = 3;

    // ���ɵ��˵�ʱ����
    public float m_timer = 0;

    protected Transform m_transform;

	// Use this for initialization
	void Start () {

        m_transform = this.transform;

	}
	
	// Update is called once per frame
	void Update () {

        // ������ɵ��˵������ﵽ���ֵ��ֹͣ���ɵ���
        if (m_enemyCount >= m_maxEnemy)
            return;

        // ÿ���һ��ʱ��
        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            m_timer = Random.value * 15.0f;
            if (m_timer < 5)
                m_timer = 5;

            // ���ɵ���
            Transform obj=(Transform)Instantiate(m_enemy, m_transform.position, Quaternion.identity);

            // ��ȡ���˵ĽǱ�
            Enemy enemy = obj.GetComponent<Enemy>();

            // ��ʼ������
            enemy.Init(this);

        }
	}

    void  OnDrawGizmos () 
    {
        Gizmos.DrawIcon (transform.position, "item.png", true);
    }

}
