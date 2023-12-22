using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExplosiveAmmo : Ammo
{
    [SerializeField] float _radius;
    [SerializeField] float _explosionForce;
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
        Collider[] items = Physics.OverlapSphere(transform.position, _radius);

        foreach (var item in items)
        {
            if (item.GetComponent<Rigidbody>() && !item.gameObject.CompareTag("Player"))
            {
                item.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _radius, 1, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
