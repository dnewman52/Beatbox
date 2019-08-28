using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptResume : MonoBehaviour
{
    public PlayerMovement playerGo;

    [SerializeField] private GameObject panelPause;

    void Start()
    {
        panelPause.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
        playerGo.GetComponent<PlayerMovement>().enabled = true;
    }
}