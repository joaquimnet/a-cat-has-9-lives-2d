using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public int unlockedLevels;

    public GameSaveData(int unlockedLevels)
    {
        this.unlockedLevels = unlockedLevels;
    }
}
