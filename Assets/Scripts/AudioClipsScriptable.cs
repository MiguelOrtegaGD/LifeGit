using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipDB", menuName = "New AudioClip DB")]
public class AudioClipsScriptable : ScriptableObject
{
    [SerializeField] List<AudioClipBase> _clips = new List<AudioClipBase>();

    public List<AudioClipBase> Clips { get => _clips; set => _clips = value; }
}

[Serializable]
public class AudioClipBase
{
    [SerializeField] string _id;
    [SerializeField] AudioClip _clip;

    public string Id { get => _id; set => _id = value; }
    public AudioClip Clip { get => _clip; set => _clip = value; }
}
