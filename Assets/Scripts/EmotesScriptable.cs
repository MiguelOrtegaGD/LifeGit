using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Emotes Settings", menuName = "New Emote Settings")]
public class EmotesScriptable : ScriptableObject
{
    [SerializeField] int emoteSelected = 1;

    public int EmoteSelected { get => emoteSelected; set => emoteSelected = value; }
}
