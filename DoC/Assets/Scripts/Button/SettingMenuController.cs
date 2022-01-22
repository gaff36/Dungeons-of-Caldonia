using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingMenuController : MonoBehaviour
{

    public Animator sfxAnimator;
    public Animator musicAnimator;
    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private GameManager gm;

    [SerializeField] private AudioSource music;

    private void Start()
    {
        gm.load();
        sfxAnimator.SetBool("on", GameManager.isSFXOn);
        musicAnimator.SetBool("on", GameManager.isMusicOn);
    }

    public void Update()
    {
        if (!GameManager.isSFXOn) AudioListener.volume = 0;
        else AudioListener.volume = 1;

        if (!GameManager.isMusicOn) music.volume = 0f;
        else music.volume = 0.147f;

        gm.load();
        sfxAnimator.SetBool("on", GameManager.isSFXOn);
        musicAnimator.SetBool("on", GameManager.isMusicOn);

    }

    public void sfxController()
    {
        GameManager.isSFXOn = !GameManager.isSFXOn;
        sfxAnimator.SetBool("on", GameManager.isSFXOn);

        gm.save();
    }

    public void musicController()
    {
        GameManager.isMusicOn = !GameManager.isMusicOn;
        musicAnimator.SetBool("on", GameManager.isMusicOn);

        gm.save();
    }
}
