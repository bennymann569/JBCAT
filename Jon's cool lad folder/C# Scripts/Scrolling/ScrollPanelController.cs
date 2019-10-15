using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollPanelController : MonoBehaviour
{
    public static float moveSpeed;
    public static bool scrolling;

    public ScrollRect myScrollRect;
    public GameObject imagePanel;


    void Update()
    {
        if (scrolling == true)
        {
            imagePanel.transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
        }
    }
}
