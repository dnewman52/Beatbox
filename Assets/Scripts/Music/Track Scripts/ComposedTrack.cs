////Generator Component
////This simply takes a given list of notes (defined in-editor) and returns that as the track

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComposedTrack : TrackInstance {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override Node GetTrack()
    {
        return TrackGenerator.BuildTrackFromList(NoteList);
    }
}
