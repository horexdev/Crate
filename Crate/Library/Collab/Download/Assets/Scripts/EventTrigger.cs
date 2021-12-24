using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool RightOrLeftButton = true;

    public static int multiplier;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RightOrLeftButton)
        {
            multiplier = 1;
        }
        else if (!RightOrLeftButton)
        {
            multiplier = -1;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        multiplier = 0;
    }
}
