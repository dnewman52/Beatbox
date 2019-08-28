////Generator Component
////The base class which all generator components must inherit
////Contains the basic functionality to be called on in order to export a track
////Implements features to allow tracks to be defined in-editor for testing

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackInstance : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual Node GetTrack() {
        return null;
    }

    [System.Serializable]
    public class SOF
    {
        public int S;
        public int O;
        public int F;
    }

    //This is our list we want to use to represent our class as an array.
    public List<SOF> NoteList = new List<SOF>(1);


    void AddNew()
    {
        //Add a new index position to the end of our list
        NoteList.Add(new SOF());
    }

    void Remove(int index)
    {
        //Remove an index position from our list at a point in our list array
        NoteList.RemoveAt(index);
    }
}
