using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serialization
{
    [System.Serializable]
    public class GameData
    {
        public int highestLevel;
        public List<int> levelScores;
    }
}