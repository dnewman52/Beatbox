using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour {
    //public Text youWin;
    [SerializeField] private GameObject panelWin;
    bool win = false;
    Color transparent = Color.clear;
    Color black = Color.black;



    // Use this for initialization
    void Start () {
        panelWin.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(win)
            panelWin.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            win = true;

    }
}
