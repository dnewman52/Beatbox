////Types
////This contains all the data types required for generation of tracks
////As well as those used in the MIDI input interface

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteName
{
    C,
    Db,
    D,
    Eb,
    E,
    F,
    Gb,
    G,
    Ab,
    A,
    Bb,
    B
}

public enum TableType
{
    AllEqual,
    RatioBasedTable,
    MajorScale
}

public enum Waveform {
    Sine,
    Square,
    Triangle,
    Sawtooth
}

public struct ADSR
{
    public float attack;
    public float decay;
    public float sustain;
    public float release;

    public ADSR(float setAttack, float setDecay, float setSustain, float setRelease)
    {
        attack = setAttack;
        decay = setDecay;
        sustain = setSustain;
        release = setRelease;
    }
}

public struct LevelEvent
{
    public float startTime; //the time at which this event will occur, measured in seconds from the start of the track
    public float duration; //how long the player should hold the note down (0 if single note)
    public NoteName note; //the key which the player should press

    public LevelEvent(float setTime, float setDuration, NoteName setNote) {
        startTime = setTime;
        duration = setDuration;
        note = setNote;
    }
}

public class Node
{
    public Node next;

    public int step;
    public int octave;
    public int frames;

    public float volume;

    public Node(int setStep, int setOctave, int setFrames, float setVolume)
    {
        next = null;
        step = setStep;
        octave = setOctave;
        frames = setFrames;
        volume = setVolume;

        while (step >= 12) {
            step -= 12;
            octave++;
            Debug.Log("stepping down");
        }

        while (step < 0) {
            step += 12;
            octave--;
            Debug.Log("stepping up");
        }
    }

    public Node(Node toCopy) {
        Copy(toCopy);
    }

    public Node(TrackInstance.SOF sof) : this(sof.S, sof.O, sof.F, 1) { }

    public void LogNode() {
        Debug.Log("[" + ToString() + "]");
    }

    public void LogSeqeunce() {
        Node pointer = new Node(this);
        string output = "[";
        do
        {
            Debug.Log("logging sequence");
            output += pointer.ToString();
            pointer = pointer.next;
        } while (pointer != null);
    }

    public override string ToString() {
        string result = ((NoteName)step).ToString() + octave;
        while (result.Length < 4) {
            Debug.Log("padding");
            result.PadLeft(1);
        }
            

        return result;

    }

    public void Insert(Node toInsert) {
        // A -> B
        //Insert X after A

        //X.next = B
        toInsert.next = next; //the inserted node gets this node's next one

        //A.next = X
        next = toInsert; //this node is now followed by the newly inserted node

        //A -> X -> B
        //X is now inbetween A and B
    }

    private void Copy(Node toCopy) {
        step = toCopy.step;
        octave = toCopy.octave;
        frames = toCopy.frames;
        next = toCopy.next;
        volume = toCopy.volume;
    }
}