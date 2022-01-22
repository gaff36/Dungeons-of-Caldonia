using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonHandler : MonoBehaviour
{
    [SerializeField] private int levelNum;
    public bool locked;

    [SerializeField] private GameObject lockedSprite;
    [SerializeField] private GameObject unLockedSprite;

    public void Awake()
    {
        if (levelNum - 1 > PlayerPrefs.GetInt("unlockedLevels"))
        {
            locked = true;
        }
        else locked = false;

        if(locked)
        {
            lockedSprite.SetActive(true);
            unLockedSprite.SetActive(false);
        }
        else
        {
            lockedSprite.SetActive(false);
            unLockedSprite.SetActive(true);
        }
    }

    public void loadScene(int sceneNumber)
    {
        if (!locked)
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
