using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    GunsSettings _gunSettingsData;

    public GunsSettings GunSettingsData { get => _gunSettingsData; set => _gunSettingsData = value; }

    private void Awake()
    {
        _gunSettingsData = Resources.Load<GunsSettings>("Scriptables/Gun Settings");
    }
}
