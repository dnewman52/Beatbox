using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenExample : MonoBehaviour {

    public float dist = 1f;
    public float time = 1f;
    Transform intent;
    MoveKeysInOctave moveKeysScript;

    // Use this for initialization
    void Start ()
    {
        intent = GetComponent<Transform>();


        for (int i = 0; i < intent.childCount-1; i++ )
        {
            Transform element = intent.GetChild(i);
            iTween.Init(element.gameObject);
        }

        moveKeysScript = gameObject.AddComponent<MoveKeysInOctave>();
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    
    public void NoteDown(string name)
    {
        Transform note = intent.transform.Find(name);
       
        Vector3 temp = new Vector3(note.position.x, note.position.y - dist, note.position.z);
        iTween.MoveFrom(note.gameObject, temp, time);
    }

    public void NoteUp(string name)
    {
        Transform note = intent.transform.Find(name);

        Vector3 temp = new Vector3(note.position.x , note.position.y + dist, note.position.z);
        iTween.MoveTo(note.gameObject, temp, time);     
    }

    public void WJ_NoteDown(string name)
    {
        Transform note = intent.transform.Find(name);

        Vector3 temp = new Vector3(note.position.x + dist, note.position.y, note.position.z);
        iTween.MoveFrom(note.gameObject, temp, time);
    }

    public void Shake()
    {
        Vector3 shake = new Vector3(0.5f, 0.5f, 0.5f);
        iTween.ShakePosition(this.gameObject, shake, 3);
    }


}
