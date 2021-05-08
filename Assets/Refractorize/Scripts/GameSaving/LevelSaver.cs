using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serialization;
using UnityEngine.UI;

public class LevelSaver : MonoBehaviour
{
    [SerializeField]
    private SaveLoadSystem saveLoadSystem;

    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private LazerActivator bonusActivator;
    [SerializeField]
    private SceneSwitcher sceneSwitcher;
    [SerializeField]
    private int levelNumber;

    private int levelScore = 1;

    [SerializeField]
    private Sprite activatedStarImagePrefab;
    [SerializeField]
    private GameObject threeLivesStarGameObject;
    private Image threeLivesStar;
    [SerializeField]
    private GameObject activatedBonusStarGameObject;
    private Image activatedBonusStar;

    [SerializeField]
    private RectTransform EndLevelScreen;

    private void Start()
    {
        threeLivesStar = threeLivesStarGameObject.GetComponent<Image>();
        activatedBonusStar = activatedBonusStarGameObject.GetComponent<Image>();
    }

    public void CompleteLevel()
    {
        Time.timeScale = 0;
        EndLevelScreen.gameObject.SetActive(true);

        if (playerStats.Lives == 3)
        {
            levelScore += 1;
            threeLivesStar.sprite = activatedStarImagePrefab;
        }
        if (bonusActivator.activated)
        {
            levelScore += 1;
            activatedBonusStar.sprite = activatedStarImagePrefab;
        }

        saveLoadSystem.Load();

        //levelNumber starts at 1, so using levelScores.Count works.

        saveLoadSystem.gameData.levelScores[levelNumber] = levelScore;
        if (!saveLoadSystem.gameData.UnlockedLevelNames.Contains(sceneSwitcher.nextLevelSceneName))
        {
           saveLoadSystem.gameData.UnlockedLevelNames.Add(sceneSwitcher.nextLevelSceneName);
        }
        saveLoadSystem.Save();

    }
}
