using System.Xml.Serialization;
using UnityEngine;

[DisallowMultipleComponent]

public class Ammo : MonoBehaviour, IFireable
{
    [SerializeField] private TrailRenderer trailRenderer;//用轨迹渲染器填充

    private float ammoRange = 0f;
    private float ammoSpeed;
    private Vector3 fireDirectionVector;
    private float fireDirectionAngle;
    private SpriteRenderer spriteRenderer;
    private AmmoDetailsSO ammoDetails;
    private float ammoChargeTimer;
    private bool isAmmoMaterialSet = false;
    private bool overrideAmmoMovement;//子弹组作为同一个实体移动

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();//缓存精灵渲染器
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

        Vector3 distanceVector=fireDirectionVector*ammoSpeed*Time.deltaTime;//计算移动弹药的距离矢量
        transform.position += distanceVector;

        ammoRange -= distanceVector.magnitude;//到达最大范围时禁用子弹

        if(ammoRange < 0f)
        {
            DisableAmmo();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DisableAmmo();
    }

    public void InitialiseAmmo(AmmoDetailsSO ammoDetails, float aimAngle, float weaponAimAngle,float ammoSpeed, Vector3 weaponAimDirectionVector, bool overrideAmmoMovement = false)
    {
        #region Ammo
        this.ammoDetails = ammoDetails;

        SetFireDirection(ammoDetails, aimAngle, weaponAimAngle, weaponAimDirectionVector);//设置开火方向
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

        gameObject.SetActive(true);//激活子弹对象

        #endregion Ammo

        #region Trail
        if (ammoDetails.isAmmoTrail)
        {
            trailRenderer.gameObject.SetActive(true);
            trailRenderer.emitting = true;
            trailRenderer.material = ammoDetails.ammoMaterial;
            trailRenderer.startWidth = ammoDetails.ammoTrailStartWidth;
            trailRenderer.endWidth= ammoDetails.ammoTrailEndWidth;
            trailRenderer.time= ammoDetails.ammoTrailTime;
        }
        else
        {
            trailRenderer.emitting= false;
            trailRenderer.gameObject.SetActive(false);
        }
        #endregion Trail
    }

    private void SetFireDirection(AmmoDetailsSO ammoDetails,float aimangle,float weaponAimAngle,Vector3 weaponAimDirectionVector)
    {
        float randomSpread=Random.Range(ammoDetails.ammoSpreadMin,ammoDetails.ammoSpreadMax);
        int spreadToggle = Random.Range(0, 2) * 2 - 1;

        if (weaponAimDirectionVector.magnitude < Settings.useAimAngleDistance)
        {
            fireDirectionAngle = aimangle;
        }
        else
        {
            fireDirectionAngle = weaponAimAngle;
        }
        fireDirectionAngle += spreadToggle * randomSpread;//子弹射出角度随机偏移

        transform.eulerAngles = new Vector3(0f, 0f, fireDirectionAngle);//设置子弹Z轴转角

        fireDirectionVector = HelperUtilities.GetDireactionVectorFromAngle(fireDirectionAngle);

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
