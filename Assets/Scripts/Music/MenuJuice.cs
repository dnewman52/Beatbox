using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuJuice : MonoBehaviour {

    public GameObject panel;
    RectTransform pos;
    Vector3 orig;
	// Use this for initialization
	void Start ()
    {
       pos = panel.GetComponent<RectTransform>();
        orig = pos.localScale;

    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private void FixedUpdate()
    {
        //pos.localScale = new Vector3(Mathf.PingPong(orig.x, orig.x + 5), Mathf.PingPong(orig.x, orig.x + 5), orig.z);
    }
}
