using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public weaponDetailsOS weaponDetails; // 武器配置
    public Transform firePoint; // 发射点位置
    public float fireRate; // 发射频率
    public float prechargeTime; // 充能时间

    private float nextFireTime; // 下一次发射的时间
    private enemyDetectPlayer playerDetector; // 用于检测玩家方向的脚本

    private void Start()
    {
        if (weaponDetails == null)
        {
            Debug.LogError("weaponDetails is not set.");
            return;
        }

        fireRate = weaponDetails.weaponFireRate;
        prechargeTime = weaponDetails.weaponPrechargeTime;

        playerDetector = GetComponentInParent<enemyDetectPlayer>();
        if (playerDetector == null)
        {
            Debug.LogError("enemyDetectPlayer script not found on the parent object.");
        }

        if (firePoint == null)
        {
            Debug.LogError("firePoint is not set.");
        }

        nextFireTime = Time.time + prechargeTime;
    }

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Fire()
    {
        if (playerDetector == null)
        {
            Debug.LogError("playerDetector is not initialized.");
            return;
        }

        AmmoDetailsSO ammoDetails = weaponDetails.weaponCurrentAmmo;
        if (ammoDetails == null)
        {
            Debug.LogError("weaponCurrentAmmo is not set in weaponDetails.");
            return;
        }

        Vector3 directionToPlayer = playerDetector.GetDirectionToPlayer().normalized;

        if (poolManager.Instance == null)
        {
            Debug.LogError("poolManager instance not found.");
            return;
        }

        if (ammoDetails.ammoPrefabArray == null || ammoDetails.ammoPrefabArray.Length == 0)
        {
            Debug.LogError("ammoPrefabArray is not set correctly in ammoDetails.");
            return;
        }

        Ammo ammo = poolManager.Instance.ReuseComponent(ammoDetails.ammoPrefabArray[0], firePoint.position, Quaternion.LookRotation(directionToPlayer)) as Ammo;
        if (ammo != null)
        {
            Debug.Log("子弹已创建！");
            // 调用 InitialiseAmmo 时传递 firePoint
            ammo.InitialiseAmmo(ammoDetails, 0f, 0f, ammoDetails.ammoSpeedMin, directionToPlayer, firePoint);
        }
        else
        {
            Debug.LogError("Failed to get ammo from the pool.");
        }
    }
}