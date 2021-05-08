using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private string menuSceneName;
    [SerializeField]
    private string nextLevelSceneName;

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Restart()
    {
        SceneManager.LoadScene(gameObject.scene.name, LoadSceneMode.Single);
    }

    public void nextScene()
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
