using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoMessage : MonoBehaviour
{
    public void ChangeMessage(string key, string text)
    {
        transform.Find("Background/TextImage/Text").GetComponent<TextMeshProUGUI>().text = text;
        transform.Find("Background/KeyImage/Key").GetComponent<TextMeshProUGUI>().text = key;
    }

    void Update()
    {
        Vector3 direction = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
