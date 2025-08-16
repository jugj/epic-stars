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
        


        transform.Translate(new Vector3(direction, 0, 0) * speed * Time.deltaTime);

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
    }
}
