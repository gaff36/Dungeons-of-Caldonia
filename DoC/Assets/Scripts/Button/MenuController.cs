using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource tapSound;
    public void playTapSound()
    {
        tapSound.Play();
    }
}
