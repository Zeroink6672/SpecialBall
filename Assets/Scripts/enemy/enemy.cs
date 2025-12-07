using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

//必须的组件
[RequireComponent(typeof(SortingGroup))]//排序组组件用于不同sprite渲染器以正确顺序排序
[RequireComponent(typeof(SpriteRenderer))]//sprite渲染器组件
[RequireComponent(typeof(Rigidbody2D))]//2D刚体组件
[RequireComponent(typeof(CircleCollider2D))]//2D圆形碰撞体组件
[RequireComponent(typeof(PolygonCollider2D))]//多边形碰撞体组件

[DisallowMultipleComponent]//不允许存在多个相同组件
public class enemy : MonoBehaviour
{
    [HideInInspector] public EnemyDetailsSO enemyDetails;
    private CircleCollider2D circleCollider2D;
    private PolygonCollider2D polygonCollider2D;
    [HideInInspector] public SpriteRenderer[] spriteRendererArray;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        spriteRendererArray = GetComponentsInChildren<SpriteRenderer>();
    }
}
