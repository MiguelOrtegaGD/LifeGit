using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameUtils : MonoBehaviour
{
    [SerializeField] UnityEvent events;

    public void ChangeSceneWithFade(string sceneName)
    {
        UIController.Instance.ChangeSceneWithFade(sceneName);
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ReloadScene(bool fade)
    {
        if (fade)
            UIController.Instance.ChangeSceneWithFade(SceneManager.GetActiveScene().name);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PlaySFX(string clipName)
    {
        AudioController.Instance.PlayMainSfx(clipName);
    }
    public void PlayMusic(string clipName)
    {
        AudioController.Instance.PlayMainMusic(clipName);
    }

    public void StopMusic()
    {
        AudioController.Instance.StopMainMusic();
    }

    public void ActivateEvents()
    {
        events?.Invoke();
    }

    public void SaveCurrentSceneData()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
    }

    public void SaveCurrentSceneData(string sceneName)
    {
        PlayerPrefs.SetInt(sceneName, 1);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
