////Track Player
////Takes a given series of nodes from a generator, and sends those notes to the oscillator at the correct time

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Oscillator))]
public class TrackPlayer : MonoBehaviour {

    public TrackInstance trackInstance;
    public List<GameObject> dependantTrackPlayers;
    private int bpm = 300;
    private float spb = 0;
    private float beatLength = 0;
    private float sustainLength = 0;
    private bool sustained = true;

    private Node track;

    private Oscillator attachedOscillator;

    private bool active = false;
    private float noteDuration = 0;
    
    // Use this for initialization
    void Start () {
        attachedOscillator = GetComponent<Oscillator>();
        spb = (60.0f / bpm);
        
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            InstantiateTrack();
            PlayTrack();
        }

        //if the track is playing
        if (active)
        {

            //if we reach the time duration for the current note
            if (sustained && (Time.time - noteDuration) >= sustainLength) {
                attachedOscillator.StopNote();
                sustained = false;
            }
            else if ((Time.time - noteDuration) >= beatLength)
            {
                track = track.next;
                PlayNote(); //calculate time duration for the next note and start playing it
            }
        }

    }

    public List<LevelEvent> GetLevelEvents() {
        List<LevelEvent> result = new List<LevelEvent>();

        InstantiateTrack();
        float timecode = 0;

        Node pointer = track;
        while (pointer != null) {

            //really basic event generation
            result.Add(new LevelEvent(

                timecode,
                pointer.frames > 2 ? pointer.frames * spb : 0,
                KeyboardInput.IntToWhiteNote(pointer.step % 5)
                
                ));

            //increment pointer twice, also increasing the current time
            timecode += (pointer.frames * spb);
            pointer = pointer.next;
            timecode += (pointer.frames * spb);
            if (pointer != null)
                pointer = pointer.next;
        }

        return result;
    }

    private bool PlayNote() {
        
        if (track != null)
        {
            attachedOscillator.PlayNote(track.step, track.octave);
            UpdateBeatLength();
            return true;
        }
        else {
            active = false; //end of track reached
            attachedOscillator.StopNote();
            return false;
        }
    }

    private void UpdateBeatLength() {
        noteDuration = Time.time;
        beatLength = track.frames * spb;
        sustainLength = beatLength * 0.75f;
        sustained = true;
    }

    private void InstantiateTrack() {
        track = null;
        if (trackInstance)
        {
            track = trackInstance.GetTrack();
        }
    }

    public void PlayTrack() {
        foreach (GameObject g in dependantTrackPlayers) {
            g.GetComponent<TrackPlayer>().PlayTrack(); //play all dependant tracks
        }

        if (track == null)
            InstantiateTrack();

        if (track != null) {
            active = true;
            PlayNote();
        }

    }
}
