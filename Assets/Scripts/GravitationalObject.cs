using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravitationalObject : MonoBehaviour
{

    [SerializeField] GravityAmmo ammo;
    Collider collision;
    Rigidbody rigid;

    bool isTrigger;

    public GravityAmmo Ammo { get => ammo; set => ammo = value; }

    public void PrepareObject(GravityAmmo controller)
    {
        rigid = GetComponent<Rigidbody>();
        collision = GetComponent<Collider>();
        isTrigger = collision.isTrigger;

        ammo = controller;
        collision.isTrigger = true;
        transform.SetParent(controller.transform);
        rigid.constraints = RigidbodyConstraints.FreezePosition;
    }

    public void DropObject()
    {
        collision.isTrigger = isTrigger;
        rigid.constraints = RigidbodyConstraints.None;
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ammo)
        {
            if (other.gameObject != ammo.gameObject)
            {
                Ammo.RemoveObject(this);
                Destroy(this);
            }
        }
    }
}
