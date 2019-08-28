using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class scriptMainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject selectedObject;
    private bool buttonSelected;
    private Button currentButton;

    // Use this for initialization
    void Start()
    {

        if (KeyboardInput.usingMidi)
        {
            Debug.Log("Midi On");
        }
        else
            Debug.Log("Midi Off");


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (KeyboardInput.KeyHeld(NoteName.C))
        {
            selectedObject = GameObject.Find("buttonStart");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            currentButton = selectedObject.GetComponent<Button>();
            Debug.Log(currentButton);
        }
        if (KeyboardInput.KeyHeld(NoteName.D))
        {
            selectedObject = GameObject.Find("buttonLevels");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            currentButton = selectedObject.GetComponent<Button>();
            Debug.Log(currentButton);
        }
        if (KeyboardInput.KeyHeld(NoteName.E))
        {
            selectedObject = GameObject.Find("buttonControls");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            currentButton = selectedObject.GetComponent<Button>();
            Debug.Log(currentButton);
        }
        if (KeyboardInput.KeyHeld(NoteName.F))
        {
            selectedObject = GameObject.Find("buttonCredits");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            currentButton = selectedObject.GetComponent<Button>();
            Debug.Log(currentButton);
        }
        if (KeyboardInput.KeyHeld(NoteName.G))
        {
            selectedObject = GameObject.Find("buttonQuit");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            currentButton = selectedObject.GetComponent<Button>();
            Debug.Log(currentButton);
        }
        if (KeyboardInput.KeyHeld(NoteName.Eb))
        {
            currentButton.onClick.Invoke();
            currentButton = null;
        }
   

    }

    private void OnDeselect()
    {   
        buttonSelected = false;
    }
}