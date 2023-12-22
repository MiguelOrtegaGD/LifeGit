using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GrabbableItem : MonoBehaviour
{
    [SerializeField] string objectName;
    GameObject message;
    bool _playerInside;
    AimStatesController _player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerInside)
        {
            _player.AddGun(objectName);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!message)
                message = UIHelper.Instance.ShowInteractMessage(gameObject, new Vector3(0, 0.3f, 0), "E", "RECOGER");
            else
                message.SetActive(true);

            _playerInside = true;
            _player = other.GetComponent<AimStatesController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            message.SetActive(false);
            _playerInside = false;
            _player = null;
        }
    }
}
