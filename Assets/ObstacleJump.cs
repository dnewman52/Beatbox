using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleJump : MonoBehaviour {
    Rigidbody rb;
    float jumpForce = 8f;
    IEnumerator coroutine;
    bool isGrounded;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        coroutine = Jump();

        StartCoroutine(coroutine);
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void LateUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, 1 << LayerMask.NameToLayer("Ground"));
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1);
    }

    IEnumerator Jump()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            rb.velocity = Vector3.up * jumpForce;
        }
    }
}
