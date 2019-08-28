using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRotate : MonoBehaviour {

    RectTransform transform;
    
	// Use this for initialization
	void Start () {
        transform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, 1) * 20 * Time.deltaTime);
    }

    void LateUpdate()
    {
        
    }
}
