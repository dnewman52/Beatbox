////Oscillator
////This code generates the appropriate sound to be played by the audio output
////Based on a given frequency, gain, and waveform, over time

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {
    public Waveform waveform;

    private float gain;
    public float volume = 0.1f;

    private double frequency;
    private double increment;
    private double phase;
    private double samplingFrequency = 48000.0;

    private void Update () {

    }

    public void PlayNote (int step, int octave) {
        if (step == -1) {
            StopNote();
        }
        else {
            frequency = KeyboardInput.NoteFrequency(step, octave);
            gain = Mathf.Max(volume * 0.05f, 0.05f);
        } 
    }

    public void StopNote () {
        gain = 0;
    }

    private void OnAudioFilterRead (float[] data, int channels) {
        //set the increment based on the frequency
        increment = frequency * 2 * Mathf.PI / samplingFrequency;

        //set the data elements to produce a sound wave
        for (int i = 0; i < data.Length; i += channels) {
            phase += increment;

            //create the waveform
            if (waveform == Waveform.Sine)
            {
                data[i] = (float)(gain * Mathf.Sin((float)phase));
            }
            else if (waveform == Waveform.Square)
            {
                if (gain * Mathf.Sin((float)phase) >= 0 * gain)
                {
                    data[i] = (float)gain * 0.6f;
                }
                else
                {
                    data[i] = (float)gain * -0.6f;
                }
            }
            else if (waveform == Waveform.Triangle) {
                data[i] = (float)(gain * (double)Mathf.PingPong((float)phase, 1.0f));
            }
            //make sure sound plays out of both speakers
            if (channels == 2) {
                data[i + 1] = data[i];
            }

            //reset the phase if it has completed a full cycle
            if (phase > (Mathf.PI * 2)) {
                phase = 0.0;
            }
        }
    }
}
