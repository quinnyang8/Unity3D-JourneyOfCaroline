using UnityEngine;
using System.Collections;

[AddComponentMenu("Game/EnemySpawn")]
public class EnemySpawn : MonoBehaviour
{
    // 敌人的Prefab
    public Transform m_enemy;

    //生成的敌人数量
    public int m_enemyCount = 0;

    // 敌人的最大生成数量
    public int m_maxEnemy = 3;

    // 生成敌人的时间间隔
    public float m_timer = 0;

    protected Transform m_transform;

	// Use this for initialization
	void Start () {

        m_transform = this.transform;

	}
	
	// Update is called once per frame
	void Update () {

        // 如果生成敌人的数量达到最大值，停止生成敌人
        if (m_enemyCount >= m_maxEnemy)
            return;

        // 每间隔一定时间
        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            m_timer = Random.value * 15.0f;
            if (m_timer < 5)
                m_timer = 5;

            // 生成敌人
            Transform obj=(Transform)Instantiate(m_enemy, m_transform.position, Quaternion.identity);

            // 获取敌人的角本
            Enemy enemy = obj.GetComponent<Enemy>();

            // 初始化敌人
            enemy.Init(this);

        }
	}

    void  OnDrawGizmos () 
    {
        Gizmos.DrawIcon (transform.position, "item.png", true);
    }

}
