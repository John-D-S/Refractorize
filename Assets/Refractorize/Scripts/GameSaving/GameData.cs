using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serialization
{
    [System.Serializable]
    public class GameData
    {
        public List<int> levelScores = new List<int>();
    }
}