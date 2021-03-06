using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public CharacterController cc;

    [Header("Player Data")]
    public float playerSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float doubleJumpMultiplier = 1f;   
    
    public Vector3 movement;
    private Vector3 velocity;

    // Ground Check
    [Header("Grounded")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Bool")]
    public bool isDashing;
    private bool isGrounded;
    private bool doubleJump = false;

    public float x;
    public float z;

    
    public static PlayerController playerInstance;
    private void Awake()
    {
        playerInstance = this;
    }
    private void Update()
    {
        
         x = Input.GetAxis("Horizontal");
         z = Input.GetAxis("Vertical");
        isDashing = Input.GetKeyDown(KeyCode.LeftShift);

        Debug.Log(x);

        movement = transform.right * x + transform.forward * z;

        cc.Move(movement * Time.deltaTime * playerSpeed);

       if(isGrounded)
        {
            doubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
       else
        {
            if (Input.GetButtonDown("Jump") && doubleJump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * doubleJumpMultiplier * -2f * gravity);
                doubleJump = false;
            }
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    



} 

