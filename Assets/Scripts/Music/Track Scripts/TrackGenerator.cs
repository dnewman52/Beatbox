////Generator Component
////This is a static class which implements the rules of a given set of AI components
////It gets rhythmic patterns and tries to produce reasonable melodies, based on the probabilities fed to it
////Tracks are a series of single linked nodes

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class TrackGenerator {
    static System.Random rnd = new System.Random(DateTime.Now.Millisecond);
    
    private static int barLength = 8;
    private static NoteName activeKey = NoteName.C;
    private static TableType activeTableType;

    private static Node RandomNode(Node bass = null, Node seed = null, int availableFrames = -1, bool mute = false)
    {
        Node result = null;

        if (bass != null)
        {
            List<int> table = TableManager.GetHarmonyTable(new NoteName[] { activeKey, KeyboardInput.StepToNote(bass.step) }, activeTableType);
            int index = 0;
            int runningTotal = table[0];

            int r = rnd.Next(0, 100);

            while (r >= runningTotal && index < table.Count) {
                index++;
                runningTotal += table[index];
            }

            int octaveShift = 0;
            int octave = 0;
            if (seed != null)
            {
                for (int i = -1; i <= 1; i++)
                {
                    if (
                        Mathf.Abs((index + (12 * i)) - seed.step) < // if the difference between the index (shifted i) octaves and the seed
                        Mathf.Abs((index + (12 * octaveShift)) - seed.step) //is less than the difference between the index (shifted o) octaves and the seed
                        )
                    {
                        octaveShift = i; //use i as the new o value for shifting
                    }
                }

                octave = seed.octave + octaveShift;
            }
            else {
                octaveShift = rnd.Next(1, 3);
                octave = bass.octave + octaveShift;
            }


            result = new Node(
                index,
                octave,
                (availableFrames == -1) ? barLength : availableFrames - rnd.Next(1, availableFrames),
                mute ? 0 : 1
            );
        }
        else {
            result = new Node(
                rnd.Next(0, 12),
                4 + rnd.Next(-1, 2),
                (availableFrames == -1) ? barLength : availableFrames - rnd.Next(1, availableFrames),
                mute ? 0 : 1
            );
        }

        return result;
    }

    public static Node GenerateMelody(Node bassTrack, TableType tableType) {
        activeTableType = tableType;
        activeKey = KeyboardInput.StepToNote(bassTrack.step); //assume the first note is the key of the song, should be computed not assumed
        barLength = 8; //should be computed not assumed
        
        Node result = null;
        Node bassPointer = null;
        Node melodyPointer = null;

        List<Node> bassBars = new List<Node>();
        List<Node> melodyBars = new List<Node>();

        int frameCount = 0;

        //determine the key frames of the bass line (first note of each bar)
        bassPointer = bassTrack;
        while (bassPointer != null) {
            //count the frames for the current note
            frameCount += bassPointer.frames;

            //if the number of frames in a bar has been reached
            if (frameCount >= barLength) {
                //record this as the root note for this bar
                bassBars.Add(bassPointer);

                //remove the excess frames
                while (frameCount >= barLength)
                {
                    frameCount -= barLength;
                }
            }

            //move to the next bass note
            bassPointer = bassPointer.next;
        }
        

        //generate key melody notes to accompany each bass note / bar
        bassPointer = bassBars[0];

        melodyPointer = RandomNode(bassBars[0]); //first node will be ignored
        Debug.Log("MELODY...");
        foreach (Node n in bassBars) {
            //get the new bass note
            bassPointer = n;

            //generate a melody keyframe based on this bass note
            melodyPointer.next = RandomNode(bassPointer, melodyPointer);
            melodyPointer = melodyPointer.next;
            //Debug.Log(melodyPointer.step + "|" + melodyPointer.octave);
            melodyBars.Add(melodyPointer);
        }

        Debug.Log("Displaying sequence...");
        //result.LogSeqeunce();
        Debug.Log("Track complete.");


        Debug.Log("RESULT...");
        result = melodyBars[0];
        melodyPointer = result;
        while (melodyPointer != null) {
            Debug.Log(melodyPointer.step + "|" + melodyPointer.octave);
            melodyPointer = melodyPointer.next;
        }
        return result;
    }

    public static Node GenerateTrack() {
        //to be changed in future
        int totalBars = 8;

        //a reference to the first note in each bar, for easier editing later
        Dictionary<int, Node> bars = new Dictionary<int, Node>();

        //the single root note which links to all following notes
        //Create first node;
        Node result = RandomNode();
        Node activePointer = result;
        result.LogNode();

        //the first node begins the first bar
        bars.Add(1, result);

        //populate the root note for each bar (starting from the second bar)
        for (int i = 2; i <= totalBars; i++) {
            //add a next node
            activePointer.next = RandomNode(activePointer);

            //shift the current pointer to the next node
            activePointer = activePointer.next;

            //store this new node as the root of a bar
            bars.Add(i, activePointer);
        }

        //insert additional notes into bars
        ////


        //Debug Log the notes
        activePointer = result;

        Debug.Log("Outputting all nodes");
        do
        {
            activePointer.LogNode();
            activePointer = activePointer.next;
        }
        while (activePointer != null);
        Debug.Log("");
        return result;

    }

    public static Node BuildTrackFromList(List<TrackInstance.SOF> notes) {
        Node result = new Node(notes[0]);
        Node pointer = result;

        for (int i = 1; i < notes.Count; i++) {
            pointer.next = new Node(notes[i]);
            pointer = pointer.next;
        }

        return result;
    }

}
