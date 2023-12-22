using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelper : Singleton<UIHelper>
{

    public GameObject ShowInteractMessage(GameObject obj, Vector3 offset, string keyText, string text)
    {
        GameObject message = Instantiate((GameObject)Resources.Load("InteractMessage"));
        message.transform.position = obj.transform.position + offset;
        message.AddComponent<InfoMessage>().ChangeMessage(keyText, text);
        message.transform.SetParent(obj.transform);
        return message;
    }
}
