//// AI Component
//// Used to calculate the dissonance between two notes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureDissonance : MonoBehaviour {

    private NoteName firstNote = NoteName.C;
    private NoteName secondNote = NoteName.C;

    private int state = 0;

	// Use this for initialization
	void Start () {
        Debug.Log("Enter first note...");
    }
	
	// Update is called once per frame
	void Update () {
        if (state == 0)
        {
            if (KeyboardInput.GetAnyKey(ref firstNote))
            {
                Debug.Log("Entered: " + firstNote.ToString());

                state = 1;
                Debug.Log("Enter second note...");
            }
        }
        else if (state == 1) {
            if (KeyboardInput.GetAnyKey(ref secondNote))
            {
                Debug.Log("Entered: " + secondNote.ToString());

                Debug.Log("Calculating Dissonance...");

                ////Ratio Based Calculation Model
                float f1 = (float)KeyboardInput.NoteFrequency(firstNote);
                float f2 = (float)KeyboardInput.NoteFrequency(secondNote);

                float d = Mathf.Min(f1, f2)/Mathf.Max(f1, f2);
                Debug.Log("Dissonance: " + d.ToString());
                
                ////Roughness Calculation Model
                ////(Not yet producing accurate results)

                //float fmin = Mathf.Min(f1, f2);
                //float fmax = Mathf.Max(f1, f2);
                //float fdif = fmax - fmin;

                //float R = 0.0f;
                //float X = 0.0f;
                //float Y = 0.0f;
                //float Z = 0.0f;

                ////assume that A is 1.0 for both waves, A min and max are both 1.0
                //X = 1;
                //Y = 1;

                //float e = Mathf.Exp(1);
                //float s = 0.24f / ((0.0207f * fmin) + 18.96f);

                //Z = (
                //    Mathf.Pow(e, (-3.5f * s) * fdif) -
                //    Mathf.Pow(e, (-5.75f * s) * fdif)
                //    );

                //R = (Mathf.Pow(X, 0.1f)) *
                //    (0.5f * Mathf.Pow(Y, 3.11f)) *
                //    (Z);

                //Debug.Log("F: " + f1.ToString() + "," + f2.ToString());
                //Debug.Log("XYZ: " + X.ToString() + "," + Y.ToString() + "," + Z.ToString());
                //Debug.Log("R: " + R.ToString());

                state = 0;
                Debug.Log("Enter first note...");
            }
        }
	}
}
