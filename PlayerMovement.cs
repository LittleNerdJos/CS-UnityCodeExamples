using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 24f;
    private bool facingRight = true;

    public static bool playerControlsEnabled = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        if (playerControlsEnabled)
        {
            //Get's a value of -1, 0, or 1 depending on the direction we're moving
            horizontal = Input.GetAxisRaw("Horizontal");

            //while someone is pressing space or up arrow
            if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
            {
                rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
            }

            //flip are character if the user changed directions
            Flip();
        }
        
    }

    //FixedUpdate can run once, zero, or several times per frame,
    //depending on how many physics frames per second are set in the time
    //settings,and how fast/slow the framerate is.

    //should be used when applying forces, torques,
    //or other physics-related functions -
    //because you know it will be executed exactly in sync
    //with the physics engine itself
    private void FixedUpdate()
    {
        //x in this vector is horizontal (direction) times speed (magnitude)
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    //this is to change the direction of the player
    //this changes the data about the direction AND the way the image is facing
    private void Flip()
    {
        //check if the facingRight bool and horizontal value don't match
        if (facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            //swap the horizontal value
            facingRight = !facingRight;

            //change the direction of the image
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    //this checks if the player is grounded
    //if the player is on the ground, then it should be able to jump
    private bool IsGrounded()
    {
        //this physics object checks if the ground check we put in
        //is overlapping with the ground by 0.2
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
