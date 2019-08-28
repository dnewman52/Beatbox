////AI Component
////Generates a rhythmic pattern given a set of parameters for bar and beat length

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class PatternGenerator {
    static System.Random rnd = new System.Random(DateTime.Now.Millisecond);

    public static void CreatePattern() {
        int barLength = 8;
        int beatLength = 2;

        int totalBars = 2;
        int density = 3; //average beats per bar over course of pattern (can be +1- total i.e. 3 * 2 bars = 6, so has a range of 5 to 7) also influences filler notes

        int patternBeats = (density * totalBars);// + rnd.Next(-1, 2);

        List<int> countBeatsInBar = new List<int>();

        for (int i = 0; i < totalBars; i++) { countBeatsInBar.Add(0); } //every bar has zero beats
        for (int i = 0; i < patternBeats; i++) { countBeatsInBar[rnd.Next(0, totalBars)]++; } //for each beat, add one beat to a random bar

        //for (int i = 0; i < totalBars; i++) { Debug.Log("Beats in Bar(" + i + ") =" + countBeatsInBar[i]); }

        int totalFrames = barLength * totalBars;
        Debug.Log("total frames " + totalFrames);
        List<bool> frameValues = new List<bool>();
        for (int i = 0; i < totalFrames; i++) frameValues.Add(false);

        List<int> pool = new List<int>();
        int totalBeats = Mathf.RoundToInt(barLength / beatLength) + 1;
        Debug.Log("total beats " + totalBeats);

        //for each bar
        for (int bar = 0; bar < totalBars; bar++) {
            //generate a new pool of possible beat locations
            pool.Clear();
            for (int p = 0; p < totalBeats; p++) { pool.Add(p * beatLength); }

            //for each beat in this bar
            for (int beat = 0; beat < countBeatsInBar[bar] && pool.Count > 0; beat++) {
                Debug.Log("new bar");
                //pick a number from the pool and add a beat to this frame
                int f = rnd.Next(0, pool.Count - 1);
                frameValues[pool[f] + (barLength * bar)] = true;
                pool.RemoveAt(f);
                Debug.Log("bar " + bar + " beat " + beat + " f" + f + " pCount" + pool.Count);

            }
            Debug.Log("bar complete");
        }


        //output result
        string output = " ";
        foreach (bool b in frameValues) {
            output += (b ? "O" : ".") + " ";
        }
        Debug.Log(output);
    }

    public static void LogPattern() {

    }
}
