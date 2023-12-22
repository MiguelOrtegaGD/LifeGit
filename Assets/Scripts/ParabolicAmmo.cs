using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParabolicAmmo : Ammo
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            OnContact();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            OnContact();
        }
    }

    public override void OnContact()
    {
        Destroy(gameObject, 2f);
    }
}
