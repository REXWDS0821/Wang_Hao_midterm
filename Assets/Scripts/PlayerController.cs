using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //"Public" variables
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 600.0f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform GroundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

    //Private Varibles
    private Rigidbody2D rBody;
    private Animator anim;
    private bool isGrounded = false;
    //face change
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    //Physics
    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        isGrounded = GroundCheck();

        //jump code goes here!
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            rBody.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        rBody.velocity = new Vector2(horiz * speed, rBody.velocity.y);

        //Check if the sprite needs to be flipped
        if ((isFacingRight && rBody.velocity.x < 0) || (!isFacingRight && rBody.velocity.x > 0))
        {
            Flip();
        }
        /*
        else if (!isFacingRight && rBody.velocity.x > 0)
        {
            Flip();
        }
        */
        //Communicate with the animator
        anim.SetFloat("xVelocity", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("yVelocity", rBody.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(GroundCheckPos.position, groundCheckRadius, whatIsGround);

    }

    //2d character face changes
    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }
}
