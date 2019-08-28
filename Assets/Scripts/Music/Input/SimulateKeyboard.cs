////MIDI Interface
////This code takes input from the user and feeds it directly to an attached oscillator
////It acts as a proof of concept for the input and allows testing of oscillator sounds and effects

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

[RequireComponent(typeof(Oscillator))]
public class SimulateKeyboard : MonoBehaviour {
    public int shiftOctave = 0;
    private Oscillator attachedOscillator;

    // Use this for initialization
    void Start () {
        attachedOscillator = GetComponent<Oscillator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.LeftShift))
            shiftOctave = 1;
        else
            shiftOctave = 0;

        foreach (NoteName n in Enum.GetValues(typeof(NoteName))) {
            if (KeyboardInput.KeyDown(n)) {
                //TrackGenerator.GenerateHarmonyTable(n, TableManager.GetTable(TableType.RatioBasedTable));
                PatternGenerator.CreatePattern();
            }

            if (KeyboardInput.KeyHeld(n)) {
                attachedOscillator.PlayNote(KeyboardInput.NoteToStep(n), 4 + shiftOctave);
                return;
            }
        }

        attachedOscillator.PlayNote(-1, 0);

    }
}
