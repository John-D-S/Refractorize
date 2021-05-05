using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serialization
{
    [System.Serializable]
    public class GameData
    {
        public int highestLevel = 0;
        public List<int> levelScores = new List<int>();
    }
}