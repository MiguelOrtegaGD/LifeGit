using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : Singleton<UIController>
{
    public void Fade(bool activate)
    {
        if (GameObject.Find("Fade"))
            GameObject.Find("Fade").GetComponent<Animator>().Play(activate ? "In" : "Out");
        else
        {
            GameObject fade = (GameObject)Instantiate(Resources.Load("Fade"), GameObject.Find("MainCanvas").transform);
            fade.GetComponent<Animator>().Play(activate ? "In" : "Out");
        }
    }

    public void ChangeSceneWithFade(string sceneName)
    {
        StartCoroutine(ChangeSceneWithFadeCoroutine(sceneName));
    }

    public IEnumerator ChangeSceneWithFadeCoroutine(string sceneName)
    {
        Fade(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
