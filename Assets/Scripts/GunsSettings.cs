using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Settings", menuName = "New Guns Configuration")]
[Serializable]
public class GunsSettings : ScriptableObject
{
    [SerializeField] List<GunProperty> _guns = new List<GunProperty>();

    public GunProperty GetGunSettingsByName(string gunName)
    {
        return _guns.Find(x => x.GunName == gunName);
    }
}