using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;



public class menuButton : MonoBehaviour {

    //button held-down controls
    //private float downTime, pressTime = 0;
    //public float countDown = 2.0f;
    //private bool ready = false;

    //initialising button and button colours
    Button backbutton;
    ColorBlock buttoncolor;
    private Color Hcolor;

    //initialising button highlight options 
    public EventSystem eventSystem;         //event system of entire scene
    private GameObject selectedObject;
    private bool buttonSelected;

    void Start ()
    {
        backbutton = gameObject.GetComponent<Button>();
        buttoncolor = backbutton.colors;
        Hcolor = buttoncolor.highlightedColor;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (KeyboardInput.KeyDown(NoteName.C) == true)
        {
            eventSystem.SetSelectedGameObject(backbutton.gameObject);
            buttonSelected = true;
        }
        //if (KeyboardInput.KeyHeld(NoteName.C) == true)
        //{
            //buttoncolor.highlightedColor = buttoncolor.pressedColor;
            //backbutton.colors = buttoncolor;
        //}
        if (KeyboardInput.KeyUp(NoteName.C)== true)
        {
            buttoncolor.highlightedColor = Hcolor;
            backbutton.colors = buttoncolor;
        }
        if (KeyboardInput.KeyDown(NoteName.Eb) == true)
        {
            buttoncolor.highlightedColor = buttoncolor.pressedColor;
            backbutton.colors = buttoncolor;
            backbutton.onClick.Invoke();
        }
    }
}
