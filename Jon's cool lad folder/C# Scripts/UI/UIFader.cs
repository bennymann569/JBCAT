using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFader : MonoBehaviour
{
    public float fadeTime;

    GameObject tempObject;
    public void FadeInPanel(GameObject g)
    {
        tempObject = g;
        g.SetActive(true);

        CanvasGroup canvasGroup = g.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        StartCoroutine(FadeUI(canvasGroup, canvasGroup.alpha, 1, fadeTime));
    }

    public void FadeOutPanel(GameObject g)
    {
        tempObject = g;

        CanvasGroup canvasGroup = g.GetComponent<CanvasGroup>();
        StartCoroutine(FadeUI(canvasGroup, canvasGroup.alpha, 0, fadeTime));
    }


    public IEnumerator FadeUI(CanvasGroup cg, float start, float end, float lerpTime)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageCompelte = timeSinceStarted / lerpTime;

        while(true)
        {
            
             timeSinceStarted = Time.time - _timeStartedLerping;
            percentageCompelte = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageCompelte);
            cg.alpha = currentValue;

            if (percentageCompelte >= 1) break;

            yield return new WaitForEndOfFrame();
        }

        if (end == 0f)
            tempObject.SetActive(false);

        if (end == 1f)
            tempObject.SetActive(true);

    }

}
