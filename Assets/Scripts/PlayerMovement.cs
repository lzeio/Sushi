using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Assingables
    public Transform playerCam;
    public Transform orientation;
    
    //Other
    private Rigidbody rb;

    //Rotation and look
    public float xRotation;
    public float sensitivity = 50f;
    public float sensMultiplier = 1f;

    //Movement
    public float moveSpeed;// = 2500f;
    public float maxSpeed = 35;
    public bool grounded;
    public LayerMask whatIsGround;
    //public float multiplier, multiplierV;

    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 35f;

    //Crouch & Slide
    public Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    public Vector3 playerScale;
    public float slideForce = 400;
    public float slideCounterMovement = 0.2f;

    //Jumping
    public bool readyToJump = true;
    public float jumpCooldown = 0.25f;
    public float crouchJump=200;
    public float jumpForce = 550f;
    //Input
    public float horizontal, vertical;
    public bool jumping, sprinting, crouching;
    
    //Sliding
     Vector3 normalVector = Vector3.up;

    //Recoil
   //public float upRecoil, sideRecoil;

    public static PlayerMovement _mInstance;
    public CapsuleCollider cp;

    void Awake() {
        _mInstance = this;
        rb = GetComponent<Rigidbody>();
        cp = GetComponent<CapsuleCollider>();
    }
    
    void Start() {
        playerScale =  transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    private void FixedUpdate() 
    {
       Movement();
    }

    private void Update() 
    {    
        MyInput();
        Look();
    }

    private void MyInput() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jumping = Input.GetKey(KeyCode.Space);
        crouching = Input.GetKey(KeyCode.C);
        sprinting = Input.GetKey(KeyCode.LeftShift);
      
        //Crouching
        if (Input.GetKeyDown(KeyCode.C))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.C))
            StopCrouch();
    }

    private void StartCrouch() {
       
       // cp.radius = cp.radius / 2;
        cp.height = cp.height / 1.5f;
        if (rb.velocity.magnitude > 0.5f) {
            if (grounded) { 
                rb.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    private void StopCrouch() {
       // cp.radius = cp.radius * 2;
        cp.height = cp.height * 1.5f;
        //transform.localScale = playerScale;
        //transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void Movement() {
    
        //Extra gravity
        rb.AddForce(Vector3.down * Time.deltaTime * 75);
        
        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteract sliding and sloppy movement
        CounterMovement(horizontal, vertical, mag);
        
        //If holding jump && ready to jump, then jump
        if (readyToJump && jumping) Jump();

        //Set max speed
        float maxSpeed = this.maxSpeed;
        
 

       
        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (horizontal > 0 && xMag > maxSpeed) horizontal = 0;
        if (horizontal < 0 && xMag < -maxSpeed) horizontal = 0;
        if (vertical > 0 && yMag > maxSpeed) vertical = 0;
        if (vertical < 0 && yMag < -maxSpeed) vertical = 0;

        //Some multipliers
        float multiplier = 1f, multiplierV = 1f;
        
        // Movement in air
        if (!grounded) {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }


        if (grounded)
        {
            moveSpeed = 500f;
            if (crouching) moveSpeed = 200f;
            if (sprinting) moveSpeed = 1000f;
        }
        //Apply forces to move player
       

        rb.AddForce(orientation.transform.forward * vertical * moveSpeed * Time.deltaTime * multiplier * multiplierV); ;
        rb.AddForce(orientation.transform.right * horizontal * moveSpeed * Time.deltaTime * multiplier);
    }

    private void Jump() {

        Debug.Log("Jump");
        if (grounded && readyToJump) {
           
            readyToJump = false;
            if(crouching)
            {
                    //Add Crouch forces
                rb.AddForce(Vector2.up * crouchJump * 1.5f);
                rb.AddForce(normalVector * crouchJump * 0.5f);

            }
            else
            {
                //Jump Forces
                rb.AddForce(Vector2.up * jumpForce * 1.5f);
                rb.AddForce(normalVector * jumpForce * 0.5f);


            }
            Vector3 vel = rb.velocity;
                if (rb.velocity.y < 0.5f)
                    rb.velocity = new Vector3(vel.x, 0, vel.z);
                else if (rb.velocity.y > 0)
                    rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);


            Invoke(nameof(ResetJump), jumpCooldown);
            
        }
       
        
    }
    
    private void ResetJump() {
        readyToJump = true;
    }
    
    private float desiredX;
    private void Look() {
       // sideRecoil = UnityEngine.Random.Range(-sideRecoil * 5, sideRecoil * 5);
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        
       
     

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;
        
        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    private void CounterMovement(float x, float y, Vector2 mag) {
        if (!grounded || jumping) return;

        //Slow down sliding
        if (crouching)
        {
            rb.AddForce(moveSpeed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }

        //Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0)) {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0)) {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }
        
        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed) {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

   
    public Vector2 FindVelRelativeToLook() {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);
        
        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v) {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;
    
    /// <summary>
    /// Handle ground detection
    /// </summary>
    private void OnCollisionStay(Collision other) {
        //Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++) {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal)) {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
        float delay = 3f;
        if (!cancellingGrounded) {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded() {
        grounded = false;
    }
    
}
