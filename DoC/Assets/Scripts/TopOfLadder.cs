using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOfLadder : MonoBehaviour
{
    private WarriorController wc;
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
         if (collision.CompareTag("Player"))
         {
            wc = collision.gameObject.GetComponent<WarriorController>();
            if (wc.rb.velocity.y > 0)
            {
                wc.touchingLadder = true;
                wc.jumpHangTime = 0.6f;

                wc.onTopOfLadder = true;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision )
    {
        if (collision.CompareTag("Player") )
        {
            wc = collision.gameObject.GetComponent<WarriorController>();
            if (wc.rb.velocity.y > 0)
            {
                wc.onTopOfLadder = true;
                wc.jumpHangTime = 0.6f;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
                wc.onTopOfLadder = false;
                wc.jumpHangTime = 0.3f;
            
        }
    }
}
