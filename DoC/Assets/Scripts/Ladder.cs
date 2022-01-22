using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private WarriorController wc;
    public BoxCollider2D bc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision == bc)
        {
            wc = collision.gameObject.GetComponent<WarriorController>();
            wc.touchingLadder = true;
            wc.horizontalLadderPosition = gameObject.transform.position.x;

            if (wc.verticalMovementInputDirection != 0 && !wc.onJumpMotion)
            {
                wc.transform.position = new Vector2(gameObject.transform.position.x, wc.transform.position.y);
                wc.climbingLadder = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision == bc)
        {
            wc = collision.gameObject.GetComponent<WarriorController>();
            wc.touchingLadder = true;


            if (wc.verticalMovementInputDirection != 0 && !wc.onJumpMotion)
            {
                wc.transform.position = new Vector2(gameObject.transform.position.x, wc.transform.position.y);
                wc.climbingLadder = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision == bc)
        {
            wc.climbingLadder = false;
            wc.touchingLadder = false;
        }
    }
}
