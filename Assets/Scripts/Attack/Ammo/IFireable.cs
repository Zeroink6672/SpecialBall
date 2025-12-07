using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFireable
{
    // 更新 InitialiseAmmo 方法的签名，增加 firePoint 参数
    void InitialiseAmmo(AmmoDetailsSO ammoDetails, float aimAngle, float weaponAimAngle, float ammoSpeed, Vector3 weaponAimDirectionVector, Transform firePoint, bool overrideAmmoMovement = false);

    // 保持 GetGameObject 方法不变
    GameObject GetGameObject();
}