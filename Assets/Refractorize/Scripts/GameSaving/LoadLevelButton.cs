using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Serialization;

[RequireComponent(typeof(Button))]
public class LoadLevelButton : MonoBehaviour
{
    [SerializeField]
    private SaveLoadSystem saveLoadSystem;
    [SerializeField]
    private List<Image> levelStars;
    [SerializeField]
    private bool loadsCutscene;
    [SerializeField]
    private string nameOfLevelToLoad;
    [SerializeField]
    private int levelNumberToLoad;

    private Button button;

    private void OnEnable()
    {
        button = gameObject.GetComponent<Button>();

        if (!loadsCutscene)
        {
            foreach (Image image in levelStars)
            {
                image.gameObject.SetActive(false);
            }
        }

        button.onClick.AddListener(LoadAssociatedScene);

        saveLoadSystem.Load();

        if (saveLoadSystem.gameData.UnlockedLevelNames.Contains(nameOfLevelToLoad))
        {
            if (!loadsCutscene)
            {
                for (int i = 0; i < saveLoadSystem.gameData.levelScores[levelNumberToLoad]; i++)
                {
                    levelStars[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            if (levelNumberToLoad != 0)
            {
                button.interactable = false;
            }
        }
    }

    public void LoadAssociatedScene()
    {
        SceneManager.LoadScene(nameOfLevelToLoad, LoadSceneMode.Single);
    }
}
