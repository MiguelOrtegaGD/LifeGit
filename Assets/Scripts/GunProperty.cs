using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[CreateAssetMenu(fileName = "Gun", menuName = "New Gun Configuration")]
[Serializable]
public class GunProperty : ScriptableObject
{
    [SerializeField] string _gunName;
    [SerializeField] GunSettings _settings;

    public string GunName { get => _gunName; set => _gunName = value; }
    public GunSettings Settings { get => _settings; set => _settings = value; }
}


[Serializable]
public class GunSettings
{
    [SerializeField] GunStats _stats;
    [SerializeField] GameObject _ammoPrefab;

    public GunStats Stats { get => _stats; set => _stats = value; }
    public GameObject AmmoPrefab { get => _ammoPrefab; set => _ammoPrefab = value; }
}


[Serializable]
public class GunStats
{
    [SerializeField] int _ammoCapacity = 10;
    [SerializeField] float _cadence = 10;
    [SerializeField] int _reloadDelay = 10;

    public int AmmoCapacity { get => _ammoCapacity; set => _ammoCapacity = value; }
    public float Cadence { get => _cadence; set => _cadence = value; }
    public int ReloadDelay { get => _reloadDelay; set => _reloadDelay = value; }
}
