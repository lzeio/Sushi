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




    
    public static PlayerController playerInstance;
    private void Awake()
    {
        playerInstance = this;
    }
    private void Update()
    {
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        isDashing = Input.GetKeyDown(KeyCode.LeftShift);

        movement = transform.right * x + transform.forward * z;

        cc.Move(movement * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
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

