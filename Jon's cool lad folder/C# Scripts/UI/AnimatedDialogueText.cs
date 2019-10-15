using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedDialogueText : MonoBehaviour
{
    private IEnumerator coroutine;
    private TextMeshProUGUI textArea;
    private string[] strings;
    public string finalString;
    private float textAnimateSpeed = 0.002f;
    public static bool textIsAnimating;
    public static int characterIndex;

    //Used for colouring important words
    public string lastCharacter;
    public string currentCharacter;

    private void Awake()
    {
        textIsAnimating = false;
    }

    void Start()
    {
        coroutine = DisplayTimer();
        StartCoroutine(coroutine);
    }

    IEnumerator DisplayTimer()
    {
        while (true)
        {
            {
                yield return new WaitForSeconds(textAnimateSpeed);
                if (characterIndex == strings.Length)
                {
                    textIsAnimating = false;
                    continue;
                }

                textArea.text += strings[characterIndex];
                characterIndex++;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void AnimateText(string input, TextMeshProUGUI currentDisplayOutput)
    {
        finalString = input;
        textIsAnimating = true;
        textArea = currentDisplayOutput;

        //take current text
       strings = new string[input.Length];
        //Populates a list of strings for each word in the output box
        for (int i = 0; i < input.Length; i++)
        {
            strings[i] = input.Substring(i, 1);
        }
        //type new text in...
       characterIndex = 0;
    }

    public void CompleteAnimateText()
    {
        while (characterIndex < strings.Length)
        {
            textArea.text += strings[characterIndex];
            characterIndex++;
        }

        textIsAnimating = false;

    }
}
