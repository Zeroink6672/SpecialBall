using System.Xml.Serialization;
using UnityEngine;

[DisallowMultipleComponent]
public class Ammo : MonoBehaviour, IFireable
{
    [SerializeField] private TrailRenderer trailRenderer; // 子弹轨迹渲染器

    private float ammoRange = 0f;
    private float ammoSpeed;
    private Vector3 fireDirectionVector;
    private float fireDirectionAngle;
    private SpriteRenderer spriteRenderer;
    private AmmoDetailsSO ammoDetails;
    private float ammoChargeTimer;
    private bool isAmmoMaterialSet = false;
    private bool overrideAmmoMovement; // 子弹是否覆盖默认运动

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 获取 SpriteRenderer
    }

    private void Update()
    {
        if (ammoChargeTimer > 0f)
        {
            ammoChargeTimer -= Time.deltaTime;
            return;
        }
        else if (!isAmmoMaterialSet)
        {
            SetAmmoMaterial(ammoDetails.ammoMaterial);
            isAmmoMaterialSet = true;
        }

        Vector3 distanceVector = fireDirectionVector * ammoSpeed * Time.deltaTime; // 更新子弹的位置
        transform.position += distanceVector;

        ammoRange -= distanceVector.magnitude; // 更新子弹的剩余范围

        if (ammoRange < 0f)
        {
            DisableAmmo();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DisableAmmo();
    }

    // 修改后的 InitialiseAmmo 函数，增加 firePoint 和 directionToPlayer 参数
    public void InitialiseAmmo(AmmoDetailsSO ammoDetails, float aimAngle, float weaponAimAngle, float ammoSpeed, Vector3 directionToPlayer, Transform firePoint, bool overrideAmmoMovement = false)
    {
        this.ammoDetails = ammoDetails;

        // 设置子弹的初始位置为发射点位置
        transform.position = firePoint.position;

        // 设置子弹的发射方向为敌人指向玩家的向量
        fireDirectionVector = directionToPlayer.normalized;

        // 设置子弹的旋转角度
        fireDirectionAngle = Mathf.Atan2(fireDirectionVector.y, fireDirectionVector.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, fireDirectionAngle);

        spriteRenderer.sprite = ammoDetails.ammoSprite;

        if (ammoDetails.ammoChargeTime > 0f)
        {
            ammoChargeTimer = ammoDetails.ammoChargeTime;
            SetAmmoMaterial(ammoDetails.ammoChargeMaterial);
            isAmmoMaterialSet = false;
        }
        else
        {
            ammoChargeTimer = 0f;
            SetAmmoMaterial(ammoDetails.ammoMaterial);
            isAmmoMaterialSet = true;
        }

        ammoRange = ammoDetails.ammoRange;
        this.ammoSpeed = ammoSpeed;
        this.overrideAmmoMovement = overrideAmmoMovement;

        gameObject.SetActive(true); // 激活子弹
    }

    private void DisableAmmo()
    {
        gameObject.SetActive(false);
    }

    private void SetAmmoMaterial(Material material)
    {
        spriteRenderer.material = material;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(trailRenderer), trailRenderer);
    }
#endif
    #endregion
}