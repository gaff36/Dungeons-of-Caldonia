using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDorr : MonoBehaviour
{
    [SerializeField] private Canvas controlCanvas;
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private WarriorController wc;
    [SerializeField] private GameManager gm;

    public void loadEndCanvas()
    {
        gm.levelPassed();
        controlCanvas.gameObject.SetActive(false);
        endCanvas.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            wc.done = true;
            loadEndCanvas();
        }
    }
}
