////Generator Component
////This simply requests a random track using a given bass line
////It calls on the static class TrackGenerator to fulfill this task
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrack : TrackInstance {

    public TableType tableType;
    public GameObject bass;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override Node GetTrack()
    {
        TrackInstance bassInstance = bass.GetComponent<TrackInstance>();
        if (bassInstance != null)
            return TrackGenerator.GenerateMelody(bassInstance.GetTrack(), tableType);
        return null;
    }
}
