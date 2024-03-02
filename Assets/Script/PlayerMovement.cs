using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField] 
    private float RunSpeed;
    [Header("Settings")]
    [SerializeField]
    private float JumpSpeedY;
    [Header("Settings")]
    [SerializeField]        
    private float JumpSpeedZ;

    //Declaring input functions
     
    //Declaring player rotation
    private Quaternion DefaultRotation;
    //Declaring player state
    enum State { Idle, Run }
    private State state;
    //Declaring player rail 
    public  enum Rail { Right, Mid, Left}
    public Rail rail;

    //For Jump 
    public float jumpHeight = 5f; // Desired jump height
    public float jumpDistance = 10f; // Desired jump distance
    public float jumpDuration = 1f; // Duration of jump animation

    private Rigidbody rb;

    private float x;
    public float SpeedDodge;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        
        DefaultRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();

        state = State.Idle;

        rail = Rail.Mid;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
            TapToStart();   
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            SwipeRight();
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            SwipeLeft();
        if (Input.GetKeyDown(KeyCode.Space))
            SwipeUp(/*JumpSpeedY, JumpSpeedZ*/);
            /*Jump();*/


        ManageState();
        ManageRail();

        
    }

    void ResetDefaultRotation()
    {
        transform.rotation = DefaultRotation;
    }

    public void SetXPosition(float newX)
    {
        // Get the current position
        Vector3 currentPosition = transform.position;

        // Set the X value of the position to the new value
        currentPosition.x = newX;

        // Update the player's position
        transform.position = currentPosition;

        /*x = Mathf.Lerp(x, newX, Time.deltaTime*SpeedDodge);*/
    }

    private void ManageState()
    {
        //Defining player state
        switch (state)
        {
            case State.Idle:
                break;

            case State.Run:
                Run();
                break;
        }
    }

    private void TapToStart()
    {
        state = State.Run;
        playerAnimator.PlayRunAnimation();
    }

    private void Run()
    {
        transform.position += Vector3.forward * RunSpeed * Time.deltaTime;
    }

    public void ManageRail()
    {
        //Rail Location
        switch(rail)
        {
            case Rail.Right:
                SetXPosition(20.15f);
                break;

            case Rail.Mid:
                SetXPosition(0f);
                break;

            case Rail.Left:
                SetXPosition(-20.15f);
                break;
        }       
    }

    public void SwipeRight()
    {
        if (rail == Rail.Left)
        {
            rail = Rail.Mid;
        }
        else if (rail == Rail.Mid)
        {
            rail = Rail.Right;
        }
    }
    public void SwipeLeft()
    {
        if (rail == Rail.Right)
        {
            rail = Rail.Mid;
        }
        else if (rail == Rail.Mid)
        {
            rail = Rail.Left;
        }
    }

    public void SwipeUp(/*float JumpSpeedY, float JumpSpeedZ*/)
    {

        // Calculate jump velocity to achieve desired height
        float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);

        // Calculate horizontal velocity based on total jump distance and jump duration
        float horizontalVelocity = jumpDistance / jumpDuration;

        // Apply the calculated velocities to the character's Rigidbody
        rb.velocity = new Vector3(horizontalVelocity, jumpVelocity, 0);

        // Play the jump animation and pass jump duration to adjust animation speed
        playerAnimator.PlayJumpAnimation(/*jumpDuration*/);


        /*if(transform.position.y <= 0.2f)
        {
            JumpUp(JumpSpeedY, JumpSpeedZ);
       */
    }


   /* private void JumpUp(float JumpSpeedY, float JumpSpeedZ)
    {
        playerAnimator.PlayJumpUpAnimation();
        transform.position += Vector3.up * JumpSpeedY * Time.deltaTime;
        JumpDown(JumpSpeedY, JumpSpeedZ);
    }

    private void JumpDown(float JumpSpeedY, float JumpSpeedZ)
    {
        playerAnimator.PlayJumpDownAnimation();
        transform.position += Vector3.down * JumpSpeedY * Time.deltaTime;
    }*/
    
    /*private void Jump()
    {
        playerAnimator.PlayJumpAnimation();
        transform.position += Vector3.forward * (2*RunSpeed) * Time.deltaTime;
    }*/

   /* public void ADDZPosition(float newZ)
    {
        // Get the current position
        Vector3 currentPosition = transform.position;

        // Set the X value of the position to the new value
        currentPosition.z += newZ;

        // Update the player's position
        transform.position = currentPosition;
    }*/

}
