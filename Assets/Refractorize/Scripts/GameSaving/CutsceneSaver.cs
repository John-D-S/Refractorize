using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serialization;
using UnityEngine.UI;

public class CutsceneSaver : MonoBehaviour
{
    [SerializeField]
    private SaveLoadSystem saveLoadSystem;
    [SerializeField]
    private SceneSwitcher sceneSwitcher;
    [SerializeField]
    private int levelNumber;

    public void CompleteCutscene()
    {
        saveLoadSystem.Load();
        if (!saveLoadSystem.gameData.UnlockedLevelNames.Contains(sceneSwitcher.nextLevelSceneName))
        {
            saveLoadSystem.gameData.UnlockedLevelNames.Add(sceneSwitcher.nextLevelSceneName);
            
        }
        saveLoadSystem.Save();
    }
}
