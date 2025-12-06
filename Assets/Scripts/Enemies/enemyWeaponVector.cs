using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Transform parentObject; // 父对象
    public float distanceFromParent = 0.7f; // 子对象与父对象中心的距离
    private Vector3 direction;
    private enemyDetectPlayer;

    private void Start()
    {
        
    }

    private void Update()
    {
        // 检测范围内带有 "player" 标签的对象
        DetectPlayer();

        // 如果检测到目标对象，更新子对象的位置和旋转
        direction=GetDirectiontoPlayer

            // 计算子对象的位置
        Vector3 childPosition = parentObject.position + direction * distanceFromParent;

            // 更新子对象的位置
        transform.position = childPosition;
    }

    private void DetectPlayer()
    {
        // 检测范围内所有带有 "player" 标签的 GameObject
        Collider[] colliders = Physics.OverlapSphere(parentObject.position, detectionRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("player"))
            {
                targetObject = collider.transform;
                return; // 找到第一个带有 "player" 标签的对象后停止检测
            }
        }

        // 如果没有检测到带有 "player" 标签的对象，将目标对象设置为 null
        targetObject = null;
    }

    private void OnDrawGizmosSelected()
    {
        // 绘制检测范围的 Gizmos
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(parentObject.position, detectionRange);
    }
}