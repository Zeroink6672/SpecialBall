using UnityEngine;

[CreateAssetMenu(fileName ="EnemyDetails_",menuName ="Scriptable Objects/Enemy/EnemyDetails")]
public class EnemyDetailsSO: ScriptableObject
{
    [Space(10)]
    [Header("BASE ENEMY DETAILS")]

    public string enemyName;//敌人名称
    public GameObject enemyPrefab;//敌人预制件

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(enemyName), enemyName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(enemyPrefab), enemyPrefab);
    }
}