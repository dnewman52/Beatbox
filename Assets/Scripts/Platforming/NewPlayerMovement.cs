using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewPlayerMovement : MonoBehaviour {

    Camera cam;
    public float PlayerSpeed = 4f;
    public float jumpForce = 250f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public const float rayCastLength = 1f;
    bool isGrounded = true;
    bool isSliding = false;
    public bool changeAnim = false;


    Vector3 move;
    Vector3 force;
    Rigidbody rb;
    RaycastHit ray = new RaycastHit();
    TweenExample tween;
    Transform player;
    Animation anim;

    private float slideHeight = 0.95f;
    
    public Dictionary<NoteName, float> keyVelocity = new Dictionary<NoteName, float>();

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        move = Vector3.left;
        player = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        foreach (NoteName n in KeyboardInput.whiteKeys) {
            keyVelocity.Add(n, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        PlayerInput();
	}

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, transform.up * -1, rayCastLength, 1 << LayerMask.NameToLayer("Ground"));
        fallModifier();
    }
    
    void OnDrawGizmos()
    {
       Gizmos.DrawLine(transform.position, transform.position + transform.up * -rayCastLength);
       Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(move*-1) * -rayCastLength);
    }


    void fallModifier()
    {
        if (rb.velocity.y < 0)
        { 
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;    
            
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        { 
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


    }

    GameObject temp;
    void PlayerInput()
    {
        Move();
        foreach (NoteName n in KeyboardInput.whiteKeys) {
            if (KeyboardInput.KeyDown(n)) {
                keyVelocity[n] = KeyboardInput.KeyVelocity(n); //store velocity at this point in time
                Debug.Log("saving velocity: " + keyVelocity[n]);
                foreach (TweenExample t in Object.FindObjectsOfType(typeof(TweenExample))) {
                    t.transform.Find(n.ToString()).transform.Translate(0, -0.75f, 0);
                }
            }

            if (KeyboardInput.KeyUp(n)) {
                foreach (TweenExample t in Object.FindObjectsOfType(typeof(TweenExample))) {
                    temp = t.transform.Find(n.ToString()).gameObject;
                    temp.transform.Translate(0, 0.75f, 0);  
                }

                if (HitDetect(ref ray, transform.up * -1)) {
                    if (ray.transform.gameObject.name == n.ToString()) {
                        //rb.velocity = Vector3.up * jumpForce * keyVelocity[n] * 2;
                    }
                }
                rb.velocity = Vector3.up * jumpForce * keyVelocity[n] * 1.5f;
                Debug.Log("get velocity: " + keyVelocity[n]);
            }
        }

    }


    void Move()
    {
        move.Normalize();

        if (move.magnitude > 0)
        {
            //if(!anim.isPlaying)
            //    anim.Play("Movement");
            this.transform.Translate(move * Time.deltaTime * PlayerSpeed);
        }
    }


    void Jump()
    {
            rb.velocity = Vector3.up * jumpForce * (isSliding ? 1.75f : 1.0f);
            rb.velocity = Vector3.up * jumpForce;
            anim.Play("Jump");
            AnimateJump();
    }

    void WallJump()
    {      
        if(HitDetect(ref ray, move))
        {
            move = Vector3.Reflect(move, Vector3.left);
            rb.velocity = Vector3.up * jumpForce;
            AnimateWallJump();
            changeAnim = !changeAnim;
        }
    }


    void ChangeDir()
    {
        if (HitDetect(ref ray, move))
        {
            move = Vector3.Reflect(move, Vector3.left);
            changeAnim = !changeAnim;
        }
    }

    void Slide()
    {
        if (!isSliding && HitDetect(ref ray, transform.up * -1))
        {
            ray.transform.Translate(0f, -1f, 0f);
            this.gameObject.transform.Translate(0f, -slideHeight, 0f);
            isSliding = true;
        }
        else if(HitDetect(ref ray, move) && isSliding)
        {
            ray.transform.Translate(0f, -slideHeight, 0f);
        }   
    }

    void Sing()
    {
        if (HitDetect(ref ray, move, 5f))
        {
            if (ray.transform.CompareTag("Obstacles"))
            {
                ray.transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                ray.transform.gameObject.GetComponent<TweenExample>().Shake();
            }
        }
    }

    void SlideStop()
    {
        if (isSliding) {
            isSliding = false;
            this.gameObject.transform.Translate(0f, slideHeight, 0f);
            rb.velocity = Vector3.up * jumpForce * 0.25f;
            if (HitDetect(ref ray, transform.up * -1)) {
                ray.transform.Translate(0f, 1f, 0f);
            }
        }     
    }


    void AnimateJump()
    {
        if (HitDetect(ref ray, transform.up * -1))
            if(ray.transform.CompareTag("Notes"))
            {
                tween = ray.transform.gameObject.GetComponentInParent<TweenExample>();
                if (tween != null)
                {
                    tween.NoteDown(ray.transform.gameObject.name);
                }
            }
    }

    void AnimateWallJump()
    {
        if (HitDetect(ref ray, -move))
        {
            if (ray.transform.gameObject.CompareTag("Notes"))
            {
                tween = ray.transform.gameObject.GetComponentInParent<TweenExample>();
                if (tween != null)
                {
                    tween.WJ_NoteDown(ray.transform.gameObject.name);
                }
            }
        }
    }

    bool HitDetect(ref RaycastHit target, Vector3 direction, float length = rayCastLength)
    {
        return Physics.Raycast(player.position, player.TransformDirection(direction), out target, length, 1 << LayerMask.NameToLayer("Ground"));
    }




    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Obstacles"))
        //    Destroy(this.gameObject);
        //if (collision.gameObject.CompareTag("Wall"))
        //    ChangeDir();
        //if (collision.transform.parent.parent.CompareTag("Wall"))
        //    WallJump();

    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        //if(isSliding)
        //{
        //    tween = collision.transform.gameObject.GetComponentInParent<TweenExample>();
        //    if (tween != null)
        //    {
        //        tween.NoteUp(collision.transform.gameObject.name);
        //    }
        //}
    }
}
