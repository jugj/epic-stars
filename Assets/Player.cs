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
    public LayerMask  groundLayer;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            direction -= accelerationspeed * Time.deltaTime;
            direction = Mathf.Clamp(direction, -1.0f, 0.0f);
        }

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            direction += accelerationspeed * Time.deltaTime;
            direction =  Mathf.Clamp(direction, 0.0f, 1.0f);
        }
        
        if (!Input.GetKey("d") && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey("a") && !Input.GetKey(KeyCode.LeftArrow)) 
        {
            direction = Mathf.MoveTowards(direction, 0, deccelerationspeed * Time.deltaTime);
        }

        transform.Translate(new Vector3(direction, 0, 0) * speed * Time.deltaTime);

        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }
}
