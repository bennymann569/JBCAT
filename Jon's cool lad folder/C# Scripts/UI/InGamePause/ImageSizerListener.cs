using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSizerListener : MonoBehaviour {
    Dropdown ImageSizeDropdown;
    public Transform panel;
    // Use this for initialization
    void Start () {
        //Fetch the Dropdown GameObject
        ImageSizeDropdown = GetComponent<Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        ImageSizeDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(ImageSizeDropdown);
        });
    }

    public void SetImagePanelSize(int v)
    {
        int x = 100;
        int y = 100;
        switch (v)
        {
            case 0:
                x = 50;
                y = 50;
                break;
            case 1:
                x = 75;
                y = 75;
                break;
            case 2:
                x = 100;
                y = 100;
                break;
        }
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);

    }
    void DropdownValueChanged(Dropdown change)
    {
        Debug.Log("DropdownValueChanged " + change.value);
        SetImagePanelSize(change.value);
    }

}
