using UnityEngine;
using System.Collections;

[AddComponentMenu("Game/Spawn")]
public class Spawn : MonoBehaviour
{

    public Transform m_transform;

    public Transform m_enemy;

    void Awake()
    {
        m_transform = this.transform;
    }



    // 图标显示
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Node.tif");
    }


}
