using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class scriptPause : MonoBehaviour {

    [SerializeField] private GameObject panelPause;

    private Button buttonPause;
    public PlayerMovement playerStop;

    void Start()
    {
        panelPause.SetActive(false);
        buttonPause = gameObject.GetComponent<Button>();
    }
    private void Update()
    {
        if (KeyboardInput.KeyDown(NoteName.Eb) == true)
        {
            buttonPause.onClick.Invoke();
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
        playerStop.GetComponent<PlayerMovement>().enabled = false;
    }
}
