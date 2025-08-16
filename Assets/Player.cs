using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float accelerationspeed;
    public float deccelerationspeed;
    public float direction;
    public bool isGrounded;
    public GameObject groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spr;

    public GameObject rightCheckobj;
    public GameObject leftCheckobj;
    public bool rightCheck;
    public bool leftCheck;
    public float lrcheckr;

    public float dashingVelocity;
    public bool isdashing;
    public bool candash;

    private float clicked = 0;
    private float clicktime = 0;
    private float clickdelay = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool leftInput = Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow);
        bool rightInput = Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow);

        if (leftInput && rightInput)
        {
            direction = Mathf.MoveTowards(direction, 0, deccelerationspeed * Time.deltaTime);
        }
        else if (!leftCheck && leftInput)
        {
            direction -= accelerationspeed * Time.deltaTime;
            direction = Mathf.Clamp(direction, -1.0f, 0.0f);
            spr.flipX = true;
        }
        else if (!rightCheck && rightInput)
        {
            direction += accelerationspeed * Time.deltaTime;
            direction = Mathf.Clamp(direction, 0.0f, 1.0f);
            spr.flipX = false;
        }
        else
        {
            direction = Mathf.MoveTowards(direction, 0, deccelerationspeed * Time.deltaTime);
        }
        
        bool touchingWall = (rightCheck || leftCheck);
        bool movingAgainstWall = (rightInput && rightCheck) || (leftInput && leftCheck);


        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        animator.SetFloat("direction", Mathf.Abs(direction));

        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);

        rightCheck = Physics2D.OverlapCircle(rightCheckobj.transform.position, lrcheckr, groundLayer);
        leftCheck = Physics2D.OverlapCircle(leftCheckobj.transform.position, lrcheckr, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            animator.SetBool("isJumping", true);
        }
        else 
        {
            animator.SetBool("isJumping", false);
        }

        animator.SetFloat("yVelocity", rb.velocity.y);
        dashing();

    }

    void dashing()
    {
        if (Input.GetButtonDown("Jump"))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            Debug.Log("dash");
        }   
    }
}
