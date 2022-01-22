using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void loadLastLevel()
    {
        SceneManager.LoadScene(2);
    }

}
