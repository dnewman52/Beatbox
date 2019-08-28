using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class scriptLevelSelect : MonoBehaviour
{
    public EventSystem eventSystem;       
    private GameObject selectedObject;
    private bool buttonSelected;
    public Button CurrentButton;

    void Start()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (KeyboardInput.KeyHeld(NoteName.C))
        {
            selectedObject = GameObject.Find("buttonLevel1");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            CurrentButton = selectedObject.GetComponent<Button>();
            Debug.Log(CurrentButton);
        }
        if (KeyboardInput.KeyHeld(NoteName.D))
        {
            selectedObject = GameObject.Find("buttonLevel2");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            CurrentButton = selectedObject.GetComponent<Button>();
            Debug.Log(CurrentButton);
        }
        if (KeyboardInput.KeyHeld(NoteName.E))
        {
            selectedObject = GameObject.Find("buttonLevel3");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            CurrentButton = selectedObject.GetComponent<Button>();
            Debug.Log(CurrentButton);
        }
        if (KeyboardInput.KeyHeld(NoteName.F))
        {
            selectedObject = GameObject.Find("buttonBack");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            CurrentButton = selectedObject.GetComponent<Button>();
            Debug.Log(CurrentButton);
        }
        if (KeyboardInput.KeyHeld(NoteName.Eb) && buttonSelected == true)
        {
            CurrentButton.onClick.Invoke();
            CurrentButton = null;
            buttonSelected = false;
        }

    }
    private void OnDisable()
    {
        buttonSelected = false;
    }
}