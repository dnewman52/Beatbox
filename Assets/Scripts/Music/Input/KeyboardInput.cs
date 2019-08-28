////MIDI Interface
////This script acts a wrapped for MIDI Jack and Unity's input methods
////It also responsible for providing conversion between values
////It requires configuration to get the correct midi channel and root note

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public static class KeyboardInput{

    private static int midiNote = 0;
    private static MidiChannel midiChannel = MidiChannel.All;
    public static bool usingMidi = false;

    private static float frequencyBase = 1.05946309436f;
    public static double NoteFrequency(int step, int octave = 4)
    {
        double result = 0;
        int octaveInSteps = ((octave - 4) * 12);
        result = 261.6 * Mathf.Pow(frequencyBase, (step + octaveInSteps));
        return result;
    }

    public static double NoteFrequency(NoteName noteName, int octave = 4) {
        return NoteFrequency(NoteToStep(noteName), octave);
    }

    public static int NoteToStep(NoteName noteName) {
        return (int)noteName;
    }

    public static NoteName StepToNote(int step) {
        return (NoteName)step;
    }

    public static int NoteToMidiInt(NoteName noteName) {
        return midiNote + (int)noteName;
    }

    public static NoteName IntToWhiteNote(int i) {
        switch (i) {
            case 0:
                return NoteName.C;
            case 1:
                return NoteName.D;
            case 2:
                return NoteName.E;
            case 3:
                return NoteName.F;
            case 4:
                return NoteName.G;
            case 5:
                return NoteName.A;
            case 6:
                return NoteName.B;
            default:
                Debug.LogError("IntToWhiteNote got a number not within the range 0 - 6");
                return NoteName.C;
        }
    }

    public static NoteName ShiftNoteBySteps(NoteName noteName, int shift) {
        NoteName result = noteName;

        int step = NoteToStep(noteName);
        step += shift;
        step = step % 12;


        result = StepToNote(step);
        return result;
    }

    public static NoteName[] whiteKeys = new NoteName[] {NoteName.C, NoteName.D, NoteName.E, NoteName.F, NoteName.G, NoteName.A, NoteName.B };

    public static KeyCode NoteToKeyCode (NoteName noteName) {
        switch (noteName) {

            case (NoteName.C):
                return KeyCode.A;

            case (NoteName.Db):
                return KeyCode.W;

            case (NoteName.D):
                return KeyCode.S;

            case (NoteName.Eb):
                return KeyCode.E;

            case (NoteName.E):
                return KeyCode.D;

            case (NoteName.F):
                return KeyCode.F;

            case (NoteName.Gb):
                return KeyCode.T;

            case (NoteName.G):
                return KeyCode.G;

            case (NoteName.Ab):
                return KeyCode.Y;

            case (NoteName.A):
                return KeyCode.H;

            case (NoteName.Bb):
                return KeyCode.U;

            case (NoteName.B):
                return KeyCode.J;

            default:
                return KeyCode.Z;
        }
    }

    public static bool KeyDown (NoteName noteName) {
        return Input.GetKeyDown(NoteToKeyCode(noteName)) || MidiMaster.GetKeyDown(midiChannel, NoteToMidiInt(noteName));
    }

    public static bool KeyUp (NoteName noteName) {
        return Input.GetKeyUp(NoteToKeyCode(noteName)) || MidiMaster.GetKeyUp(midiChannel, NoteToMidiInt(noteName));
    }

    public static bool KeyHeld (NoteName noteName) {
        return Input.GetKey(NoteToKeyCode(noteName)) || MidiMaster.GetKey(midiChannel, NoteToMidiInt(noteName)) != 0;
    }

    public static float KeyVelocity (NoteName noteName) {
        float v = MidiMaster.GetKey(midiChannel, NoteToMidiInt(noteName));
        if (!usingMidi || v == 0) {
            return (Input.GetKey(NoteToKeyCode(noteName)) ? 1 : 0);
        }
        else
            return v;     
    }

    private static List<NoteName> GetKeysDown() {
        List<NoteName> results = new List<NoteName>();

        foreach (NoteName n in Enum.GetValues(typeof(NoteName))) {
            if (KeyDown(n))
                results.Add(n);
        }

        return results.Count > 0 ? results : null;
    }

    public static bool GetAnyKey(ref NoteName key) {
        bool result = false;
        List<NoteName> held = GetKeysDown();

        if (held != null) {
            result = true;
            key = held[0];
        }

        return result;
    }

    public static bool ConfigMidi() {

        foreach (MidiChannel mCh in Enum.GetValues(typeof(MidiChannel)))
        {
            for (int note = 0; note < 127; note++)
            {
                if (MidiMaster.GetKey(mCh, note) != 0)
                {
                    usingMidi = true;
                    midiChannel = mCh;
                    midiNote = note;

                    Debug.Log("Configuring midi controller...");
                    Debug.Log("Using midi channel [" + midiChannel.ToString() + "]...");
                    Debug.Log("Using midi root note [" + midiNote.ToString() + "]...");

                    return true;
                }
            }
        }

        if (Input.anyKey) {
            if (Input.GetKey(KeyCode.A))
            {
                usingMidi = false;

                Debug.Log("Configuring computer keyboard...");
                Debug.Log("Using default keyboard setup...");

                return true;
            }
        }


        return false;
    }
}
