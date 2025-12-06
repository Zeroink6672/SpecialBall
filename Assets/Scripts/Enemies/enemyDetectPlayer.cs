using UnityEngine;

public class enemyDetectPlayer : MonoBehaviour
{
    public Transform parentObject; // 父对象
    public float detectionRadius = 5f; // 检测半径

    private Vector3 directionToPlayer; // 父对象与最近 player 之间的向量
    private Transform nearestPlayer; // 最近的 player 对象

    private void Start()
    {
        // 确保子对象的 CircleCollider2D 存在并设置为触发器
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider == null)
        {
            Debug.LogError("CircleCollider2D is missing on the child object.");
        }
        else
        {
            collider.radius = detectionRadius;
            collider.isTrigger = true;
        }
    }

    private void Update()
    {
        // 检测范围内最近的 player 对象
        DetectNearestPlayer();

        // 如果找到 player，计算方向向量
        if (nearestPlayer != null)
        {
            directionToPlayer = nearestPlayer.position - parentObject.position;
        }
        else
        {
            Debug.Log("没找到！");
            directionToPlayer = Vector3.zero; // 如果没有找到 player，方向向量设置为零
        }

        // 可选：在调试模式下输出方向向量
        //Debug.Log(directionToPlayer);
    }

    private void DetectNearestPlayer()
    {
        // 获取父对象位置
        Vector3 parentPosition = parentObject.position;

        // 初始化最近 player 的距离和 Transform
        float nearestDistance = Mathf.Infinity;
        nearestPlayer = null;

        // 使用 Physics2D.OverlapCircle 获取范围内的所有碰撞器
        Collider2D[] colliders = Physics2D.OverlapCircleAll(parentPosition, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // 计算父对象与当前 player 的距离
                float distance = Vector3.Distance(parentPosition, collider.transform.position);

                // 如果当前 player 更近，更新最近 player
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestPlayer = collider.transform;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 绘制检测范围的 Gizmos
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(parentObject.position, detectionRadius);
    }

    // 提供一个方法来获取方向向量
    public Vector3 GetDirectionToPlayer()
    {
        return directionToPlayer;
    }
}