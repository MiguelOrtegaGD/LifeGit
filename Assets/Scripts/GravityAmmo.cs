using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravityAmmo : Ammo
{
    [SerializeField] float _speed;
    [SerializeField] float _transitionSpeed;
    [SerializeField] float _rotationSpeed;

    //[SerializeField] List<GameObject> items = new List<GameObject>();
    [SerializeField] Dictionary<GravitationalObject, float> items = new Dictionary<GravitationalObject, float>();
    [SerializeField] List<GameObject> usedItems = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);

        if (items.Count > 0)
        {
            foreach (var item in items)
            {
                if (Vector3.Distance(transform.position, item.Key.transform.position) > item.Value)
                {
                    item.Key.transform.position = Vector3.MoveTowards(item.Key.transform.position, transform.position, _transitionSpeed * Time.deltaTime);
                }

                item.Key.transform.RotateAround(transform.position, Vector3.forward, _rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() && !usedItems.Contains(other.gameObject) && other.CompareTag("Item"))
        {
            GravitationalObject gravityAmmo = other.gameObject.AddComponent<GravitationalObject>();
            gravityAmmo.PrepareObject(this);
            items.Add(gravityAmmo, Vector3.Distance(transform.position, other.transform.position) - 0.001f);
            usedItems.Add(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!usedItems.Contains(collision.gameObject))
        {
            OnContact();
        }
    }

    public void RemoveObject(GravitationalObject obj)
    {
        if (items.ContainsKey(obj))
        {
            obj.DropObject();
            items.Remove(obj);
        }
    }

    public override void OnContact()
    {
        foreach (var item in items)
        {
            item.Key.DropObject();
        }

        items.Clear();
        enabled = false;
        Destroy(gameObject);
    }
}
