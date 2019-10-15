using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Required when Using UI elements.
public class ScrollController : MonoBehaviour
{
    public ScrollRect myScrollRect;
    public Scrollbar newScrollBar;

    public void Start()
    {
        //Change the current vertical scroll position.
        myScrollRect.verticalNormalizedPosition = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CheckScroll(Vector2 scrollRect)
    {

        //Debug.Log("CheckScroll: " + scrollRect.ToString() + " ? " + myScrollRect.viewport.transform.position.ToString() + " top: " + myScrollRect.viewport.rect.ToString());
       if (myScrollRect.viewport.transform.position.y < 0f)
        {
            Vector2 newpos = myScrollRect.viewport.transform.position;
            newpos.y = 0f;
            myScrollRect.viewport.transform.position = newpos;

        }

        if (myScrollRect.viewport.transform.position.y > 100f)
        {
            Vector2 newpos = myScrollRect.viewport.transform.position;
            newpos.y = 100f;
            myScrollRect.viewport.transform.position = newpos;

        }
    }

}