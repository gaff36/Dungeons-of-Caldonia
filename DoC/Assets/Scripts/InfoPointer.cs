using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPointer : MonoBehaviour
{
    private BoxCollider2D bc;
    [SerializeField] private GameObject info;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            info.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            info.SetActive(false);
        }
    }

}
