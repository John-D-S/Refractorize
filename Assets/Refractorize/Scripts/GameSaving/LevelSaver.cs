using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serialization;
using UnityEngine.UIElements;

public class LevelSaver : MonoBehaviour
{
    [SerializeField]
    private SaveLoadSystem saveLoadSystem;

    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private LazerActivator bonusActivator;
    [SerializeField]
    private int levelNumber;

    private int levelScore = 1;

    [SerializeField]
    private Texture activatedStarImagePrefab;
    [SerializeField]
    private Image threeLivesStar;
    [SerializeField]
    private Image activatedBonusStar;

    [SerializeField]
    private RectTransform EndLevelScreen;
   

    public void CompleteLevel()
    {
        Time.timeScale = 0;
        EndLevelScreen.gameObject.SetActive(true);

        if (playerStats.Lives == 3)
        {
            levelScore += 1;
            threeLivesStar.image = activatedStarImagePrefab;
        }
        if (bonusActivator.activated)
        {
            levelScore += 1;
            activatedBonusStar.image = activatedStarImagePrefab;
        }

        //levelNumber starts at 1, so using levelScores.Count works.
        if (saveLoadSystem.gameData.levelScores.Count < levelNumber)
        {
            saveLoadSystem.gameData.levelScores.Add(levelScore);
            saveLoadSystem.Save();
        }
        else
        {
            saveLoadSystem.gameData.levelScores[levelNumber - 1] = levelScore;
        }
    }
}
