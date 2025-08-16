using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool candash;
    public bool isdashing;
    public float couldown;
    public TrailRenderer Trail;

    private bool leftInput, rightInput;

    public Vector3 lastcheckpoint;
    public Image fadeImage;
    public float fadeDuration = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        lastcheckpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        leftInput = Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow);
        rightInput = Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow);

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


        if (!isdashing) rb.velocity = new Vector2(direction * speed, rb.velocity.y);

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
        
        if(!isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("dash");
            if(candash) StartCoroutine(dashing());
        }

        if(isdashing) 
        {
            Trail.enabled = true;
        }
        else
        {
            Trail.enabled = false;
        }

    
    }

    public IEnumerator dashing()
    {    
        candash = false;
        isdashing = true;

        if(!leftInput && !rightInput)
        {
            rb.velocity = new Vector2(rb.velocity.x, dashingVelocity);
        }
        else if(rightInput)
        {
            rb.velocity = new Vector2(dashingVelocity, rb.velocity.y);
        }
        else if(leftInput)
        {
            rb.velocity = new Vector2(-dashingVelocity, rb.velocity.y);
        }
        
        yield return new WaitForSeconds(0.5f);
        isdashing = false;

        yield return new WaitForSeconds(couldown);
        candash = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "death")
        {
            StartCoroutine(death());
        }
    }

    public IEnumerator death() 
    {   
        animator.SetBool("Die", true);
        StartCoroutine(Fade(0, 1));
        yield return new WaitForSeconds(0.5f);;
        transform.position = lastcheckpoint;
        yield return new WaitForSeconds(0.5f);;
        animator.SetBool("Die", false);
        StartCoroutine(Fade(1, 0));
        
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }
}
