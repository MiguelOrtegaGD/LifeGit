using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicGun : Gun
{
    [SerializeField] float _shootForce;

    public float ShootForce { get => _shootForce; set => _shootForce = value; }


    public override void Shoot(Transform target)
    {
        Vector3 neededRotation = target.position - ShootPivot.position;
        ShootPivot.rotation = Quaternion.LookRotation(neededRotation);
        GameObject ammoObj = Instantiate(Settings.AmmoPrefab, ShootPivot.position, ShootPivot.rotation, null);
        ammoObj.GetComponent<Rigidbody>().velocity = ShootPivot.forward * _shootForce;
    }
}
