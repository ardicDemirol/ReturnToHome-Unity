using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    float currentSpeed = 1f;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float runSpeed = 20f;
    [SerializeField] float jumpPower = 100f;




    private SpriteRenderer spriteRenderer;
    private Vector2 movement;





    private void Awake()
    {
        currentSpeed = walkSpeed;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        Move();
        SpeedUp();
    }




    void Move()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(movement.x, movement.y);

        FlipCharcter();


        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    spriteRenderer.flipX = true;
        //    //movement = new Vector2(-1,0);
        //    rb.velocity = Vector2.zero;
        //    rb.AddForce(movement * currentSpeed * Time.deltaTime);

        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    spriteRenderer.flipX = false;
        //    //movement = new Vector2(1,0);
        //    rb.velocity = Vector2.zero;
        //    rb.AddForce(movement * currentSpeed * Time.deltaTime);

        //}

    }



    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.gravityScale = 0.85f;
            movement = new Vector2(rb.velocity.x, jumpPower);

            if (rb.velocity.x >= 0)
            {
                rb.AddForce(movement * Time.deltaTime);

            }
            if (rb.velocity.x < 0)
            {
                rb.AddForce(-movement * Time.deltaTime);
            }

        }
        else if (rb.velocity.y < -0.1f)
        {
            rb.gravityScale = 3f;

            Debug.Log("D���yorum");
        }

    }

    void SpeedUp()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
            rb.AddForce(movement * currentSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
        else
        {
            currentSpeed = walkSpeed;
            rb.AddForce(movement * currentSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private void FlipCharcter()
    {
        if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }







    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "")
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "")
        {

        }
    }


}
