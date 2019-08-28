using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGeneration : MonoBehaviour {

    public GameObject node;
    public float speed = 5f;
    public float pace = 1f;
    IEnumerator gameplay;

	// Use this for initialization
	void Start () {

        gameplay = Spawn(pace);
        StartCoroutine(gameplay);

	}
	
	// Update is called once per frame
	void Update () {
		
	}



    IEnumerator Spawn(float time)
    {
        while(true)
        {
            yield return new WaitForSeconds(time);
            GameObject temp;


            temp = Instantiate(node, this.transform);

            Rigidbody motion = temp.GetComponent<Rigidbody>();

            motion.velocity = this.transform.TransformDirection(-Vector3.forward * speed);
        }
    }
}
