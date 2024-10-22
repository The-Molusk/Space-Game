using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    #region Variables
    Vector3 velocity, walljump; 
    public LayerMask groundMask, crouchMask;
    float wallJumpHeight = 4f, wallJumpForce = 20f, slowDown = 1f, benis, gravStrength, crouchDistance = 0.2f, groundDistance = 0.2f;
    public float speed = 0f;
    bool dJump, wallDoubleCheck, onWall, isGrounded, isCrouching, canUnCrouch = true;
    int slowDownSpeed = 1;
    float dashTimer = 3f;
    float currentDashLength = 0f;
    bool isDashing;
    float x, z;
    Health health;
    float posX, posY, posZ;
    GameObject dashPoint;
    public GameObject EventSystem;
    GlobalValues Global;

    [SerializeField] GameObject weapons, dashDelayText;
    [SerializeField] CharacterController controller;
    [SerializeField] Transform groundCheck, crouchCheck;
    [SerializeField, Range(0f, 10f)] float jumpHeight = 3f; 
    [SerializeField, Range(0f, 100f)] float gravity = 30f; 
    [SerializeField, Range(0f, 25f)] float walkSpeed = 12f; 
    [SerializeField, Range(0f, 50f)] float sprintSpeed = 20f;
    [SerializeField, Range(0f, 100f)] float dashSpeed = 50f;
    [SerializeField, Range(0f, 5f)] float dashLength = 0.5f;
    [SerializeField, Range(0f, 10f)] float dashRecharge = 1f;
    [SerializeField] bool canDoubleJump, canWallJump;
    [SerializeField] int dashCount;

    [SerializeField] GameObject O, N, NE, E, SE, S, SW, W, NW;
    #endregion
    private void Start()
    {
        Global = EventSystem.GetComponent<GlobalValues>();
        health = GetComponent<Health>();
        benis = slowDownSpeed / 10000f;
        gravStrength = gravity * -1;
        dashTimer = dashCount;
        currentDashLength = dashLength;
    }
    void Update()
    {
        dashTimer += (Time.deltaTime*dashRecharge); //altering dash timers
        currentDashLength += Time.deltaTime;

        if (dashTimer > dashCount)
        {
            dashTimer = dashCount; //maximum dashes
        }
        dashDelayText.GetComponent<TMPro.TextMeshProUGUI>().text = dashTimer.ToString(); //on-screen dispalys
        if (isGrounded) //resets double jump and disables wall jump when you touch ground
        {
            dJump = true;
            wallDoubleCheck = false;
        }
        if (Input.GetKey(KeyCode.LeftControl) && !isDashing) //u got ctrl?
        {
            transform.localScale = new Vector3(1f, 0.4f, 1f); //u go smol. easy way to crouch.
            weapons.transform.localScale = new Vector3(1f, 2.5f, 1f); //weapons are scaled up cus im fucking retarded and cba to make a proper fix, just dont look up or down with weapons out
            if (isGrounded)//doesnt slow u down if you crouch midair, we have the cool ccs crouch jump possiblities now
            {
                isCrouching = true;
                speed = walkSpeed; //slow
            }
        }
        if (!Input.GetKey(KeyCode.LeftControl)&& canUnCrouch == false && !isDashing) //u dont got ctrl?
        {
            transform.localScale = new Vector3(1f, 1f, 1f); //u go normal
            weapons.transform.localScale = new Vector3(1f, 1f, 1f); //returns shit back
            if (isGrounded)
            { speed = sprintSpeed; isCrouching = false; }
        }

        //old sprint stuff, replaced by dash now
        
        //if (Input.GetKey(KeyCode.LeftShift) && (isGrounded && !Input.GetKey(KeyCode.LeftControl) && canUnCrouch == false)) //u got shift
        //{ speed = sprintSpeed; }

        //if (Input.GetKeyUp(KeyCode.LeftShift) && (isGrounded && canUnCrouch == false)) // u dont got shift?
        //{ speed = walkSpeed; }


        //checks if your on ground annd if you can uncrouch (anythign above you to block u)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        canUnCrouch = Physics.CheckSphere(crouchCheck.position, crouchDistance, crouchMask);

        //gravity
        if (isGrounded && velocity.y < 0) //if the player is on ground, and its velocity on the y-axis is under 0:
        {
            velocity.y = -2f; //maks sure u dont blast downwards at 600000mph when u walk off a platform
        }

        //movement
        if (!isDashing)
        {
            x = Input.GetAxis("Horizontal"); //just gets ur wasd inputs by default
            z = Input.GetAxis("Vertical");
        }

        if(z != 0 && x != 0 && !isDashing)
        {
            z = z / 1.25f; //normally diagonal movement would make you faster, since you have 2 forces. this is retarded, so this fixes it.
            x = x / 1.25f; //1.25 may not be perfect, but its good enoguh taht you dont feel it
        }
        Vector3 move = transform.right * x + transform.forward * z; //creates a movement vector
        controller.Move(move * speed * Time.deltaTime);  //moves the player, multiplying the movemt speed by the speed set.
        walljump = walljump * slowDown;

        if (move.magnitude > 0.5)
        {
            Global.moveCheck = true;
        }
        else
        {
            Global.moveCheck = false;
        }

        //dashing:
        //this code is the most abhorrent thing known to all of mankind

        //horizontal velocity calc doesnt work too well for some reason, ill figure this out later
        float dispX = transform.position.x - posX; //gets chnage in position since last frame, this is important to caluclate velocity, since the player isnt using a rigidbody
        float dispY = transform.position.y - posY;
        float dispZ = transform.position.z - posZ;
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
        float horizontalVelocity = Mathf.Sqrt(dispX*dispX + dispZ*dispZ) / Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer >= 1 && !isCrouching)
        {
            isDashing = true;
            dashTimer -= 1;
            currentDashLength = 0f;
            health.GiveIFrames(dashLength);

            int front = 0;
            int right = 0;

            // i sincerely apologise to all professional programmers for creating this monstrosity of inefficient code:
            if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            {
                front = -1;
            }
            else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                front = 1;
            }

            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                right = -1;
            }
            else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                right = 1;
            }
            switch (front)
            {
                case 0:
                    switch (right)
                    {
                        case 0:
                            dashPoint = N;
                            break;
                        case 1:
                            dashPoint = E;
                            break;
                        case -1:
                            dashPoint = W;
                            break;
                    }
                    break;
                case 1:
                    switch (right)
                    {
                        case 0:
                            dashPoint = N;
                            break;
                        case 1:
                            dashPoint = NE;
                            break;
                        case -1:
                            dashPoint = NW;
                            break;
                    }
                    break;
                case -1:
                    switch (right)
                    {
                        case 0:
                            dashPoint = S;
                            break;
                        case 1:
                            dashPoint = SE;
                            break;
                        case -1:
                            dashPoint = SW;
                            break;
                    }
                    break;
            }
        }

        if (currentDashLength < dashLength)
        {
            Vector3 direction = dashPoint.transform.position - O.transform.position;
            currentDashLength += Time.deltaTime;
            velocity.y = 0;
            controller.Move(direction * Time.deltaTime * dashSpeed);
        }
        else
        {
            currentDashLength = dashLength;
            isDashing = false;
        }
        

        //checking if you can walljump
        if (onWall)
        {
            if (slowDown > 0)
            {
                slowDown = slowDown - benis;
            }
            else
            {
                onWall = false;
                slowDown = 0;
            }

        }
        controller.Move(walljump * wallJumpForce * Time.deltaTime);
        
        //jumping
        if (Input.GetButtonDown("Jump") && (isGrounded || (dJump && canDoubleJump)))
        {
            if (!isGrounded)
            {
                dJump = false;
            }
            if (wallDoubleCheck)
            {
                dJump = true;
                wallDoubleCheck = false;
            }
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravStrength); //jumps
        }
   
        velocity.y += gravStrength * Time.deltaTime; //adds on the gravity to the jump
    
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) //on colllision with shit, this case for walls
    {
        if (!isGrounded && hit.normal.y < 0.3f )
        {
            if (Input.GetButtonDown("Jump") && hit.gameObject.tag == "wall" && canWallJump) //if u jump and if u touching wall
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red, 5f); //draws a ray from where u hit (this is currently pointless)
                velocity.y = Mathf.Sqrt(wallJumpHeight * -2 * gravStrength); //jumps
                walljump = hit.normal;
                slowDown = 1f;
                onWall = true;
                wallDoubleCheck = true;
                
            }
            
        }

        //stupid ass contact damage shit

        //if(hit.gameObject.tag == "enemy")
        //{
        //    Debug.DrawRay(hit.point, hit.normal, Color.red, 5f); //draws a ray from where u hit (this is currently pointless)
        //    velocity.y = Mathf.Sqrt(wallJumpHeight * -2 * gravStrength); //jumps
        //    walljump = hit.normal;
        //    slowDown = 1f;
        //    onWall = true;
        //    wallDoubleCheck = true;
        //}
    }
    
}