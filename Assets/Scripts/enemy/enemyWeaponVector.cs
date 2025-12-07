
using UnityEngine;

public class enemyWeaponVector : MonoBehaviour
{
    public Transform parentObject; // 父对象
    public float distanceFromParent = 0.7f; // 子对象与父对象中心的距离
    private Vector3 direction;
    private enemyDetectPlayer playerDetector;

    private void Start()
    {
        playerDetector = parentObject.GetComponent<enemyDetectPlayer>();
    }

    private void Update()
    {
        // 检测范围内带有 "Player" 标签的对象以获得方向
        direction = playerDetector.GetDirectionToPlayer().normalized;
        Debug.Log(direction);
        // 计算子对象的位置
        Vector3 childPosition = parentObject.position + direction * distanceFromParent;

        // 更新子对象的位置
        transform.position = childPosition;
    }
}