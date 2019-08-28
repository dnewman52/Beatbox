using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKeysInOctave : MonoBehaviour {

    List<Transform> keyTransforms;
    Dictionary<string, float> delayTimes;

	// Use this for initialization
	void Start () {
        keyTransforms = new List<Transform>();
        delayTimes = new Dictionary<string, float>();
        foreach (Transform t in transform) {
            if (!t.gameObject.name.Contains("#")) {
                keyTransforms.Add(t);
                delayTimes.Add(t.name, 0);
            }
        }
        	
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Transform t in keyTransforms) {
            if (delayTimes[t.name] == 0) {
                if (t.localPosition.y < -3.5f) {
                    t.Translate(Vector3.up * Time.deltaTime * 4.5f);
                }
                else {
                    t.localPosition = new Vector3(t.localPosition.x, -3.5f, t.localPosition.z);
                }
            }
            else {
                delayTimes[t.name] -= Time.deltaTime;
                if (delayTimes[t.name] < 0)
                    delayTimes[t.name] = 0;
            }  
        }
        	
	}

    public void SetDelay (string name, float d) {
        delayTimes[name] = d;
    }
}
