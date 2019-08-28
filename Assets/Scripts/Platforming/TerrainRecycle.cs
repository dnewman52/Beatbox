using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRecycle : MonoBehaviour {
    Vector3 tempVector = new Vector3(-20, 0, 0);

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("Trigger");
            //other.gameObject.transform.TransformVector(other.transform += tempVector);
            Transform temp = other.gameObject.GetComponent<Transform>();
            temp.position += tempVector;

        }
       

    }
}
