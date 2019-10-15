using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHoverLeft : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
	public float speed = 50f;

	public GameObject ImagePanel; 

	bool b;

	//    private Vector2 LeftPosition;

	//public float xPosition, yPosition;

	//private float increaseXPosition, increaseYPosition;

	public void  OnPointerEnter(PointerEventData eventData)
	{
		b = true;
		Debug.Log("Mouse is over GameObject.");
	}

	public void  OnPointerExit(PointerEventData eventData)
	{
		b = false;

		Debug.Log ("Laughing At this");
	}

	void Update ()
	{
		if(b == true)
		{
			ImagePanel.transform.Translate (speed * Time.deltaTime, 0f,0f);
		}      
	}    
}
