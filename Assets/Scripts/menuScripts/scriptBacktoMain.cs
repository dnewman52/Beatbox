using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class scriptBacktoMain : MonoBehaviour
{ 
    public EventSystem eventSystem;
    private GameObject selectedObject;
    private bool buttonSelected = false;
    private Button currentButton;

    void Start()
    {
       
    }

    void Update()
    {
        if (KeyboardInput.KeyHeld(NoteName.C))
        {
            selectedObject = GameObject.Find("buttonBack");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            currentButton = selectedObject.GetComponent<Button>();
            Debug.Log(currentButton);
        }
        if (KeyboardInput.KeyHeld(NoteName.Eb))
        {
            currentButton.onClick.Invoke();
            currentButton = null;
            buttonSelected = false;
        }
           
    }
}