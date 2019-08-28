using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class scriptWinLose : MonoBehaviour
{

    public scriptPause pausescript;
    [SerializeField] private GameObject panelWinLose;

    public EventSystem eventSystem;
    private GameObject selectedObject;
    private bool buttonSelected;
    public Button CurrentButton;
    public AudioClip death;
    AudioSource music;
    bool soundplayed = false;

    // Use this for initialization
    void Start()
    {
        music = GetComponent<AudioSource>();
        panelWinLose.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (panelWinLose.activeSelf == true)
        {
            if(!soundplayed)
            {
                music.PlayOneShot(death);
                soundplayed = true;
            }
            pausescript.GetComponent<scriptPause>().enabled = false;
            if (KeyboardInput.KeyDown(NoteName.C))
            {
                selectedObject = GameObject.Find("buttonRestart");
                eventSystem.SetSelectedGameObject(selectedObject);
                buttonSelected = true;
                CurrentButton = selectedObject.GetComponent<Button>();
             
            }
            if (KeyboardInput.KeyDown(NoteName.D))
            {
                selectedObject = GameObject.Find("buttonBack");
                eventSystem.SetSelectedGameObject(selectedObject);
                buttonSelected = true;
                CurrentButton = selectedObject.GetComponent<Button>();
                
            }
            if (KeyboardInput.KeyDown(NoteName.Eb))
            {
                CurrentButton.onClick.Invoke();
            }

        }
    }
}