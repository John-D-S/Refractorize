using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serialization
{
    [System.Serializable]
    public class GameData
    {
        public List<string> UnlockedLevelNames = new List<string>();
        public List<int> levelScores = new List<int>() { 0, 0, 0, 0, 0};
    }
}