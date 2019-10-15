using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float directionSpeed;


    public void OnPointerEnter(PointerEventData eventData)
    {
        ScrollPanelController.moveSpeed = directionSpeed;
        ScrollPanelController.scrolling = true;
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ScrollPanelController.scrolling = false;
    }
}
