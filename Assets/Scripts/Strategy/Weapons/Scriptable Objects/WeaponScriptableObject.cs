using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon", order = 0)]
public class WeaponScriptableObject : ScriptableObject
{
    public GunType type;
    public new string name;
    public GameObject modelPrefab;
    public Vector3 spawnPoint;
    public Vector3 spawnRotation;
    public Vector3 bulletSpawnPoint;
    public WeaponAmmo weaponAmmo;

    public WeaponConfiguration weaponConfig;
    public BulletConfiguration bulletConfig;
    public AudioConfigWeapon audioConfig;
    
    private MonoBehaviour activeMonoBehaviour;
    private GameObject model;
    private float lastShootTime;
    private int currentAmmo = 0;
    private BulletFactory bulletFactory;
    private AudioSource audioSource;

    private void OnEnable()
    {
        currentAmmo = weaponAmmo.maxAmmo;
        
        if (bulletConfig != null && bulletConfig.bulletPrefab != null)
        {
            bulletFactory = new BulletFactory();
            bulletFactory.Initialize(bulletConfig.bulletPrefab.GetComponent<Bullet>(), bulletConfig);
        }
    }

    public void ReloadWeapon()
    {
        currentAmmo = weaponAmmo.maxAmmo;
    }

    public void RemoveWeapon()
    {
        if (model != null)
        {
            Destroy(model);
            model = null;
        }
    }
    
    public void Spawn(Transform parent, MonoBehaviour activeMonoBehaviour)
    {
        this.activeMonoBehaviour = activeMonoBehaviour;
        lastShootTime = -weaponConfig.fireRate;
        
        model = Instantiate(modelPrefab);
        model.transform.SetParent(parent, false);
        model.transform.localPosition = spawnPoint;
        model.transform.localRotation = Quaternion.Euler(spawnRotation);

        audioSource = model.AddComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (HasAmmo())
        {
            if (weaponConfig.fireMode == WeaponConfiguration.FireMode.SingleShot)
            {
                if (Time.time > weaponConfig.fireRate + lastShootTime)
                {
                    lastShootTime = Time.time;
                    FireBullet();
                }
            }
            else if (weaponConfig.fireMode == WeaponConfiguration.FireMode.Auto)
            {
                if (Time.time > weaponConfig.fireRate + lastShootTime)
                {
                    lastShootTime = Time.time;
                    FireBullet();
                }
            }
            else if (weaponConfig.fireMode == WeaponConfiguration.FireMode.Burst)
            {
                if (Time.time > weaponConfig.fireRate + lastShootTime)
                {
                    lastShootTime = Time.time;
                    activeMonoBehaviour.StartCoroutine(BurstFire());
                }
            }
            else if (weaponConfig.fireMode == WeaponConfiguration.FireMode.Shotgun)
            {
                if (Time.time > weaponConfig.fireRate + lastShootTime)
                {
                    lastShootTime = Time.time;
                    for (int i = 0; i < weaponConfig.shotgunBulletCount; i++)
                    {
                        FireBullet();
                    }
                }
            }
        }
    }

    private void FireBullet()
    {
        currentAmmo -= weaponAmmo.ammoSpentPerShot;
        
        audioConfig.PlayShootingSound(audioSource);
        
        Vector3 shootDirection = -model.transform.forward + new Vector3(
            Random.Range(-weaponConfig.spread.x, weaponConfig.spread.x),
            Random.Range(-weaponConfig.spread.y, weaponConfig.spread.y),
            Random.Range(-weaponConfig.spread.z, weaponConfig.spread.z));

        shootDirection.Normalize();
    
        Vector3 spawnPosition = model.transform.TransformPoint(bulletSpawnPoint);
        GameObject bullet = bulletFactory.Create(spawnPosition, Quaternion.LookRotation(shootDirection));
    }
    
    private IEnumerator BurstFire()
    {
        for (int i = 0; i < weaponConfig.burstBulletCount; i++)
        {
            FireBullet();
            yield return new WaitForSeconds(weaponConfig.burstInterval);
        }
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }
}
