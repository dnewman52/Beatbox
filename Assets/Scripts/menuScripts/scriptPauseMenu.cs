using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;


public class scriptPauseMenu : MonoBehaviour
{

    //initialising button highlight options 
    public EventSystem eventSystem;         //event system of entire scene
    private GameObject selectedObject;
    private bool buttonSelected;
    private Button currentButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyboardInput.KeyDown(NoteName.C))
        {
            selectedObject = GameObject.Find("buttonResume");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            currentButton = selectedObject.GetComponent<Button>();
            Debug.Log(currentButton);
        }
        if (KeyboardInput.KeyDown(NoteName.D))
        {
            selectedObject = GameObject.Find("buttonRestart");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            currentButton = selectedObject.GetComponent<Button>();
            Debug.Log(currentButton);
        }
        if (KeyboardInput.KeyDown(NoteName.E))
        {
            selectedObject = GameObject.Find("buttonMainMenu");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            currentButton = selectedObject.GetComponent<Button>();
            Debug.Log(currentButton);
        }
        if (buttonSelected == true && KeyboardInput.KeyDown(NoteName.Eb) == true)
        {
            //currentButton = selectedObject.GetComponent<Button>();
            Time.timeScale = 1;
            currentButton.onClick.Invoke();

        }
    }
}