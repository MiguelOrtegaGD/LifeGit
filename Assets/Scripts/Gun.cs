using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] string gunName;
    [SerializeField] GunSettings settings;

    [SerializeField] Transform shootPivot;

    float cadenceTimer = 0;
    float reloadTimer = 0;
    [SerializeField] float currentCharger;

    public string GunName { get => gunName; set => gunName = value; }
    public Transform ShootPivot { get => shootPivot; set => shootPivot = value; }
    public GunSettings Settings { get => settings; set => settings = value; }

    private void Start()
    {
        Settings = DataManager.Instance.GunSettingsData.GetGunSettingsByName(gunName).Settings;
        currentCharger = Settings.Stats.AmmoCapacity;
    }

    private void Update()
    {
        cadenceTimer = cadenceTimer > 0 ? cadenceTimer -= Time.deltaTime : 0;

        if (reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;

            if (reloadTimer <= 0)
            {
                reloadTimer = Settings.Stats.ReloadDelay;
            }
        }
    }

    public void CanShoot(Transform target)
    {
        if (reloadTimer <= 0 && cadenceTimer <= 0 && currentCharger > 0)
        {
            Shoot(target);
            cadenceTimer = Settings.Stats.Cadence;
        }
    }

    public virtual void Shoot(Transform target)
    {
        UpdatePivotPosition(target);
        GameObject ammoObj = Instantiate(Settings.AmmoPrefab, ShootPivot.position, Quaternion.identity, null);
        ammoObj.transform.rotation = ShootPivot.rotation;
    }

    public void UpdatePivotPosition(Transform target)
    {
        Vector3 neededRotation = target.position - ShootPivot.position;
        ShootPivot.rotation = Quaternion.LookRotation(neededRotation);
    }

    public virtual void Reload()
    {
        if (currentCharger < Settings.Stats.AmmoCapacity)
        {
            reloadTimer = Settings.Stats.ReloadDelay;
        }
    }

    public virtual void StopReload()
    {
        reloadTimer = 0;
    }

    public void GetStats(GunSettings newSettings)
    {
        Settings = newSettings;
    }
}
