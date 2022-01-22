using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool isSFXOn;
    public bool isMusicOn;
    public int unlockedLevels;

    public GameData(GameManager gm)
    {
        isSFXOn = GameManager.isSFXOn;
        isSFXOn = GameManager.isMusicOn;
        unlockedLevels = GameManager.unlockedLevels;
    }
}
