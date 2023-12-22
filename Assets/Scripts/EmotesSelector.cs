using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotesSelector : MonoBehaviour
{
    [SerializeField] EmotesScriptable emotes;
    [SerializeField] Animator model;

    public void SelectEmote(int emoteIndex)
    {
        emotes.EmoteSelected = emoteIndex;
        model.CrossFade(emoteIndex.ToString(), 0.1f);
        Debug.Log("xD", model.gameObject);
    }

}
