using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Serialization;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private string menuSceneName;
    public string nextLevelSceneName;

    private SaveLoadSystem SaveLoadSystem;

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Restart()
    {
        SceneManager.LoadScene(gameObject.scene.name, LoadSceneMode.Single);
    }

    public void NextScene()
    {
        SwitchScene(nextLevelSceneName);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(menuSceneName, LoadSceneMode.Single);
    }

    private void Start()
    {
        //this prevents new scenes from being loaded in paused;
        Time.timeScale = 1;
    }
}
