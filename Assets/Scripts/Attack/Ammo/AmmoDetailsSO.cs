using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AmmoDetails_",menuName ="Scriptable Objects/Weapons/Ammo Details")]
public class AmmoDetailsSO : ScriptableObject
{
    [Space(10)]
    [Header("BASIC AMMO DETAILS")]

    public string ammoName;
    public bool isPlayerAmmo;

    [Space(10)]
    [Header("AMMO SPRITE, PREFAB & MATERIALS")]

    public Sprite ammoSprite;
    [Tooltip("填入用于弹药的预制件。如果指定了多个预制件，将从数组中随机选择一个预制件。预制件可以是弹药组，只要它符合 IFireable 接口。")]
    public GameObject[] ammoPrefabArray;
    public Material ammoMaterial;//子弹使用的材质，Material类型封装了着色器和纹理

    public float ammoChargeTime = 0.1f;//如果弹药在移动前需要短暂“充能”，则设置弹药在发射后保持充能状态的秒数，再进行释放
    public Material ammoChargeMaterial;//可能的蓄力时子弹材质

    [Space(10)]
    [Header("AMMO BASE PARAMETERS")]
    public int ammoDamage = 1;//每颗子弹造成的伤害
    public float ammoSpeedMin = 20f;
    public float ammoSpeedMax = 20f;//子弹的最大速度和最小速度-从二者间随机取值

    public float ammoRange = 20f;//子弹存在的最大范围
    public float ammoRotationSpeed = 1f;//子弹旋转速度（角度每秒）

    [Space(10)]
    [Header("AMMO SPREAD DETAILS")]
    public float ammoSpreadMin = 0f;
    public float ammoSpreadMax = 0f;//子弹的最大和最小散射角度-从二者间随机取值

    [Space(10)]
    [Header("AMMO SPAWN DETAILS")]
    public int ammoSpawnAmountMin = 1;
    public int ammoSpawnAmountMax = 1;//每次发射时最大和最小子弹生成数量-随机取值

    public float ammoSpawnIntervalMax = 0f;
    public float ammoSpawnIntervalMin = 0f;//子弹生成的间隔（秒）

    [Space(10)]
    [Header("AMMO TRAIL DETAILS")]
    public bool isAmmoTrail = false;//子弹是否存在轨迹-制作射线武器时会用于留下持续的轨迹
    public float ammoTrailTime = 3f;//轨迹存在时间
    public Material ammoTrialMaterial;//轨迹的材质
    [Range(0f, 1f)] public float ammoTrailStartWidth;
    [Range(0f, 1f)] public float ammoTrailEndWidth;

    private void OnValidate()
    {
        // 验证字符串是否为空
        HelperUtilities.ValidateCheckEmptyString(this, nameof(ammoName), ammoName);

        // 验证对象是否为 null
        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoSprite), ammoSprite);        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoMaterial), ammoMaterial);
        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoChargeMaterial), ammoChargeMaterial);
        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoTrialMaterial), ammoTrialMaterial);

        // 验证数组是否为空
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(ammoPrefabArray), ammoPrefabArray);

        // 验证值是否为正数
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoChargeTime), ammoChargeTime);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoDamage), ammoDamage);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoSpeedMin), ammoSpeedMin);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoSpeedMax), ammoSpeedMax);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoRange), ammoRange);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoRotationSpeed), ammoRotationSpeed);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoTrailTime), ammoTrailTime);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoTrailStartWidth), ammoTrailStartWidth);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoTrailEndWidth), ammoTrailEndWidth);

        // 验证散射角度和生成数量的合理性
        if (ammoSpreadMin > ammoSpreadMax)
        {
            Debug.LogWarning($"[{this.name}] {nameof(ammoSpreadMin)} should be less than or equal to {nameof(ammoSpreadMax)}.", this);
        }

        if (ammoSpawnAmountMin > ammoSpawnAmountMax)
        {
            Debug.LogWarning($"[{this.name}] {nameof(ammoSpawnAmountMin)} should be less than or equal to {nameof(ammoSpawnAmountMax)}.", this);
        }

        if (ammoSpawnIntervalMin > ammoSpawnIntervalMax)
        {
            Debug.LogWarning($"[{this.name}] {nameof(ammoSpawnIntervalMin)} should be less than or equal to {nameof(ammoSpawnIntervalMax)}.", this);
        }
    }
}
