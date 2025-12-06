using UnityEngine;

[CreateAssetMenu(fileName ="WeaponDetails_",menuName ="Scriptable Objects/Attack/Weapon Details")]
public class weaponDetailsOS : ScriptableObject
{
    [Space(10)]
    [Header("WEAPON BASE DETAILS")]

    [Tooltip("武器射击位置-子弹发射位置一般绑定在武器末端，该数据表示偏移量")]
    public Vector3 weaponShootPosition;

    [Tooltip("武器所用子弹")]
    public AmmoDetailsSO weaponCurrentAmmo;

    [Tooltip("武器开火速度")]
    public float weaponFireRate = 0.2f;

    [Tooltip("武器充能时间（开火延迟）")]
    public float weaponPrechargeTime = 0f;

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(weaponCurrentAmmo), weaponCurrentAmmo);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(weaponFireRate), weaponFireRate);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(weaponPrechargeTime), weaponPrechargeTime);
    }
}
