using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Inventory", menuName = "New Gun Inventory")]
public class GunInventory : ScriptableObject
{
    [SerializeField] List<string> _gunNames = new List<string>();

    public List<string> GunNames { get => _gunNames; set => _gunNames = value; }

    public void AddGun(string gunName)
    {
        if (!GunNames.Contains(gunName))
        {
            GunNames.Add(gunName);
        }
    }
}
