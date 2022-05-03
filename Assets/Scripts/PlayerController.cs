using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public CharacterController cc;

    public ZombieData zz;


    public float playerSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private Vector3 velocity;

    // Ground Check

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool isDashing;
    private bool isGrounded;

    public Vector3 movement;

    public float playerHealth;

    //Instance
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
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

        if(playerHealth<=0)
        {
            Debug.Log("Lol ");
        }
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

