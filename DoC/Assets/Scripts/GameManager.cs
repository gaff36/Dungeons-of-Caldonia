using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private int sceneID;
    public static bool isSFXOn;
    public static bool isMusicOn;
    public static int unlockedLevels;
    public static int maxLevels;

    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas deadCanvas;

    [SerializeField] private AudioSource music;

    private void Start()
    {
        maxLevels = 9;

        load();

       if(!isMusicOn) music.volume = 0f;
       if(!isSFXOn) AudioListener.volume = 0;

        Application.targetFrameRate = 60;
        resumeGame();
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
    }

    public void reloadScene()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneID);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

 

    public void loadNextScene()
    {  
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % (maxLevels + 1));     
    }
    
    public void startDeadCanvas()
    {
        deadCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);    
    }

   public void save()
    {
        if(isSFXOn) PlayerPrefs.SetInt("sfxKey", 0);
        else PlayerPrefs.SetInt("sfxKey", 1);
        if(isMusicOn) PlayerPrefs.SetInt("musicKey", 0);
        else PlayerPrefs.SetInt("musicKey", 1);
    }

    public void load()
    {
        if (PlayerPrefs.GetInt("sfxKey") == 0)
        {
            isSFXOn = true;
        }
        else isSFXOn = false;

        if (PlayerPrefs.GetInt("musicKey") == 0)
        {
            isMusicOn = true;
        }
        else isMusicOn = false;
    }

    public void levelPassed()
    {
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("unlockedLevels"))
        {
            PlayerPrefs.SetInt("unlockedLevels", SceneManager.GetActiveScene().buildIndex);
        }
    }

}
