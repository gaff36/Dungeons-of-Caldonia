using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonController : MonoBehaviour
{
    [SerializeField] private WarriorController wc;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        wc.pressJumpButton();
    }

}
