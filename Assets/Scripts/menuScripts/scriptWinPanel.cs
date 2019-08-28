using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class scriptWinPanel : MonoBehaviour
{

    
    [SerializeField] private GameObject panelWin;

    public EventSystem eventSystem;
    private GameObject selectedObject;
    private bool buttonSelected;
    private Button CurrentButton;
    
    public Button pauseButton;
    public PlayerMovement pMovement;
    
    // Use this for initialization
    void Start()
    {
        panelWin.SetActive(false);

    }




    private void Update()
    {
        if (panelWin.activeSelf == true)
        {
            Time.timeScale = 0;
            pMovement.GetComponent<PlayerMovement>().enabled = false;
            pauseButton.gameObject.SetActive(false);
            if (KeyboardInput.KeyHeld(NoteName.C))
            {
                selectedObject = GameObject.Find("buttonNext");
                eventSystem.SetSelectedGameObject(selectedObject);
                buttonSelected = true;
                CurrentButton = selectedObject.GetComponent<Button>();
                
            }
            if (KeyboardInput.KeyHeld(NoteName.D))
            {
                selectedObject = GameObject.Find("buttonBack");
                eventSystem.SetSelectedGameObject(selectedObject);
                buttonSelected = true;
                CurrentButton = selectedObject.GetComponent<Button>();
                
            }
            if (KeyboardInput.KeyDown(NoteName.Eb) && buttonSelected == true)
            {
                CurrentButton.onClick.Invoke();
                Time.timeScale = 1;
                pMovement.GetComponent<PlayerMovement>().enabled = true;
            }

        }
    }
}


