using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string imageID;
    public string manualText;
    public InteractableObject targetIneractableObject;
    string outputFinal;
    private GameObject tipBox;
    private GameObject tipBoxRight;
    public bool tipActive;
    GameController controller;
    TextMeshProUGUI textBox;

    //RectTransform canvasRect;

    private static List<string> outputList = new List<string>();

    void Awake()
    {
        tipBox = GameObject.Find("Tipbox");
        textBox = GameObject.Find("ToolTipText").GetComponent<TextMeshProUGUI>();
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Start()
    {
        tipBox.SetActive(false);
    }

    void Update()
    {
        if (controller.hacking == false && controller.talking == false && PauseMenu.gameIsPaused == false)
        {
            tipActive = true;
        }

        else
        {
            tipActive = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tipActive == true)
        {
            ShowToolTip();
            controller.tipActiveGC = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        controller.tipActiveGC = false;
    }

    public void ShowToolTip()
    {
        if (manualText != null)
        {
            outputList.Add(manualText);
        }

        if (targetIneractableObject != null)
        {
            outputList.Add("[" + targetIneractableObject.noun + "]");
            outputList.Add(targetIneractableObject.description);
            if (targetIneractableObject.interactions.Length != 0)
            {
                outputList.Add("The following commands work on the " + targetIneractableObject.noun + ": " + "\n");

                foreach (Interaction i in targetIneractableObject.interactions)
                {
                    outputList.Add(i.inputAction.name);
                }
            }
        }

        outputFinal = string.Join("\n", outputList.ToArray());
        textBox.text = outputFinal;
        outputList.Clear();
    }
}
