using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour {

    private GameObject player;
    private GameObject endPt;
    private Vector3 offset;
    private bool endOfLevel = false;

    [SerializeField] private GameObject panelLose;

    public float lag = 0;

    Color transparent = Color.clear;
    Color black = Color.black;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;

        panelLose.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (checkDeath())
        {
            panelLose.SetActive(true);
        }
        
    }

    private void LateUpdate()
    {
        if(!checkDeath())
            transform.position = player.transform.position + offset;
    }


    bool checkDeath()
    {
        if (player.gameObject == null)
            return true;
        else
            return false;
    }

    bool checkWin()
    {
        if (endOfLevel)
            return true;
        else return false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
