using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is used to test that the list of level events coming from the track player makes sense
//a real level would grab this list of events and use it to generate obstacles which require the corresponding keys to be pressed
public class TestLevel : MonoBehaviour {
    private float startTime;
    private bool active = false;
    private List<LevelEvent> levelEvents;
    private int currentEvent = 0;

    public TrackPlayer trackPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            startTime = Time.time;
            active = true;
            levelEvents = trackPlayer.GetLevelEvents(); //get the list of events i.e. when the player should press each key
            trackPlayer.PlayTrack(); //start actually playing the audio for the track
            Debug.Log("START OF LEVEL");
        }

        //for the sake of testing, a debug log is produced every time the player should have pressed a key (i.e. this is when an obstacle should be in their way)
        if (active) {
            if ((Time.time - startTime) >= levelEvents[currentEvent].startTime) {
                Debug.Log("TIME["+ (Time.time - startTime).ToString() +"] PRESS " + levelEvents[currentEvent].note.ToString() + " FOR " + levelEvents[currentEvent].duration + " SECONDS.");
                currentEvent++;

                if (currentEvent >= levelEvents.Count) {
                    active = false;
                    Debug.Log("END OF LEVEL");
                }
            }
        }
	}

    public void LogLevelEvents() {

    }
}
