using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private Scene menu;
    [SerializeField]
    private Scene nextScene;

    public void SwitchScene(Scene scene)
    {
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }

    public void Restart()
    {
        SceneManager.LoadScene(gameObject.scene.name, LoadSceneMode.Single);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(gameObject.scene.name, LoadSceneMode.Single);
    }
}
