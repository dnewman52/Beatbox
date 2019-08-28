using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    Camera cam;
    public float PlayerSpeed = 4f;
    public float jumpForce = 250f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public const float downRayCastLength = 0.1f;
    public const float moveRayCastLength = 1f;
    bool isGrounded = true;
    bool isSliding = false;
    public bool changeAnim = false;


    Vector3 move;
    Vector3 force;
    Rigidbody rb;
    RaycastHit ray = new RaycastHit();
    TweenExample tween;
    Transform player;
    Animator anim;
    AudioSource audio;
    public ParticleSystem singParticle;

    public AudioClip[] clips;

    private float slideHeight = 0.95f;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        move = Vector3.left;
        player = GetComponent<Transform>();
        anim = GetComponentInParent<Animator>();
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        PlayerInput();
	}

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, transform.up * -1, downRayCastLength, 1 << LayerMask.NameToLayer("Ground"));
        fallModifier();
    }
    
    void OnDrawGizmos()
    {
       Gizmos.DrawLine(transform.position, transform.position + transform.up * -downRayCastLength);
       Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(move*-1) * -moveRayCastLength);
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



    void PlayerInput()
    {
        Move();
        if (KeyboardInput.KeyHeld(NoteName.C) && isGrounded) //Keyboard A
            Jump();
        else if (KeyboardInput.KeyHeld(NoteName.C) && HitDetect(ref ray, move, 2) && !isGrounded)
            Jump();
        if (KeyboardInput.KeyHeld(NoteName.D) && isGrounded) //Keyboard S
            Slide();
        if (KeyboardInput.KeyUp(NoteName.D)) 
            SlideStop();
        if (KeyboardInput.KeyHeld(NoteName.E)) //Keyboard D
            Sing();

    }


    void Move()
    {
        move.Normalize();

        if (move.magnitude > 0)
        {
            //anim.Play("Movement");
            this.transform.Translate(move * Time.deltaTime * PlayerSpeed);
        }
    }


    void Jump()
    {
        rb.velocity = Vector3.up * jumpForce * (isSliding ? 1.75f : 1.0f);
        AnimateJump();
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
            //ray.transform.Translate(0f, -1f, 0f);
            SetKeyTransformDown(ray.transform);

            this.gameObject.transform.Translate(0f, -slideHeight, 0f);
            isSliding = true;
        }
        else if(isSliding)
        {
            if (HitDetect(ref ray, (move + Vector3.down) * 1.5f))
                SetKeyTransformDown(ray.transform, true);

            if (HitDetect(ref ray, move * 1.5f))
                SetKeyTransformDown(ray.transform, true);

            if (HitDetect(ref ray, move * -1.5f))
                SetKeyTransformDown(ray.transform, true);

            if (HitDetect(ref ray, Vector3.down))
                SetKeyTransformDown(ray.transform, true);
        }   
    }

    void Sing()
    {

        if (HitDetect(ref ray, move, 5f))
        {
            if (ray.transform.CompareTag("Obstacles"))
            {
                ray.transform.gameObject.GetComponentInChildren<CapsuleCollider>().isTrigger = true;
                //ray.transform.gameObject.GetComponentFromChildren<CapsuleCollider>().isTrigger = true;
                ray.transform.gameObject.GetComponent<TweenExample>().Shake();
            }
        }
    }

    void SlideStop()
    {
        if (isSliding && isGrounded) {
            isSliding = false;
            this.gameObject.transform.Translate(0f, slideHeight, 0f);
            rb.velocity = Vector3.up * jumpForce * 0.25f;

            if (HitDetect(ref ray, transform.up * -1)) {
                //ray.transform.Translate(0f, 1f, 0f);
                SetKeyTransformUp(ray.transform);
            }
        }
    }

    private MoveKeysInOctave moveKeysScript;

    void SetKeyTransformDown (Transform t, bool delay = false) {
        t.localPosition = new Vector3(t.localPosition.x, -4.5f, t.localPosition.z);

        if (delay) {
            moveKeysScript = t.gameObject.GetComponentInParent<MoveKeysInOctave>();
            moveKeysScript.SetDelay(t.name, 0.1f);
        }    
    }

    void SetKeyTransformUp (Transform t) {
        t.localPosition = new Vector3(t.localPosition.x, -3.5f, t.localPosition.z);
    }

    void AnimateJump()
    {
        if (HitDetect(ref ray, transform.up * -1))
            if(ray.transform.CompareTag("Notes"))
            {
                //ray.transform.Translate(Vector3.down);
                SetKeyTransformDown(ray.transform);

                PlayNote();
                //tween = ray.transform.gameObject.GetComponentInParent<TweenExample>();
                //if (tween != null)
                //{
                //    PlayNote();
                //    tween.NoteDown(ray.transform.gameObject.name);
                //}
            }
    }

    /*
    void AnimateWallJump()
    {
        if (HitDetect(ref ray, -move))
        {
            if (ray.transform.gameObject.CompareTag("Notes"))
            {
                tween = ray.transform.gameObject.GetComponentInParent<TweenExample>();
                if (tween != null)
                {
                    PlayNote();
                    tween.WJ_NoteDown(ray.transform.gameObject.name);
                }
            }
        }
    }
    */

    bool HitDetect(ref RaycastHit target, Vector3 direction, float length = moveRayCastLength)
    {
        return Physics.Raycast(player.position, player.TransformDirection(direction), out target, length, 1 << LayerMask.NameToLayer("Ground"));
    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles") && !isSliding)
            Destroy(this.gameObject);
            
        if (collision.transform.parent.gameObject.CompareTag("Wall"))
            ChangeDir();
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if(isSliding)
        {
            //tween = collision.transform.gameObject.GetComponentInParent<TweenExample>();
            //if (tween != null)
            //{
            //    SlidePlayNote();
            //    tween.NoteUp(collision.transform.gameObject.name);
            //}
        }
    }

    void PlayNote()
    {
        float num = Mathf.Abs(Mathf.Sin(Time.time*2)) * (clips.Length - 1);
        int clipNum = Mathf.FloorToInt(num);

        audio.clip = clips[clipNum];
        audio.Play();
    }

    void SlidePlayNote()
    {
        float num = Mathf.Abs(Mathf.Sin(Time.time*2)) * (clips.Length - 1);
        int clipNum = Mathf.FloorToInt(num);

        audio.clip = clips[clipNum];
        audio.PlayOneShot(audio.clip);
    }
}
