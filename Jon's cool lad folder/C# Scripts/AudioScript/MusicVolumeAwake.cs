using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeAwake : MonoBehaviour
{
    public GameController controller;
    public Slider slider;

    void Awake()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        slider.value = controller.roomNavigation.musicVolume;
    }
}
