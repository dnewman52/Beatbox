using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LoadSceneOnClick))]
public class StartScreen : MonoBehaviour {
    private bool waiting = true;
    private void Start()
    {
        Debug.Log("Press C");
    }

    private void Update()
    {
        if (waiting && KeyboardInput.ConfigMidi()) {
            
            waiting = false;
            Debug.Log("Loading Game...");
            GetComponent<LoadSceneOnClick>().LoadByName("sceneMainMenuSide");
        }
    }
}
