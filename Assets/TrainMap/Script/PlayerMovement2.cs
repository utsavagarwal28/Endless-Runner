using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PlayerAnimator playerAnimator;
    [Header("Settings")]
    [SerializeField] private float RunSpeed;

    //Physics and RigidBody
    private Rigidbody rb;
    private Vector3 currentVelocity;
    Vector3 currentPosition;

    //Declaring player state
    enum State { Idle, Run, Jump, Fall }
    private State state;
    //Declaring player rail 
    public enum Rail { Right, Mid, Left }
    public Rail rail;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();

        state = State.Idle;
        rail = Rail.Mid;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.S))
            TapToStart();
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            SwipeRight();
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            SwipeLeft();
        if (Input.GetKeyDown(KeyCode.Space))
            SwipeUp();
    }

    private void FixedUpdate()
    {

        ManageState();
        ManageRail();
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

            case State.Jump:
                Jump();
                break;

            case State.Fall:
                Fall();
                break;
        }
    }

    public void ManageRail()
    {
        Vector3 currentPosition = transform.position;
        //Rail Location
        switch (rail)
        {
            case Rail.Right:
                rb.MovePosition(new Vector3(20.15f, currentPosition.y, currentPosition.z));
                break;

            case Rail.Mid:
                rb.MovePosition(new Vector3(0f, currentPosition.y, currentPosition.z));
                break;

            case Rail.Left:
                rb.MovePosition(new Vector3(-20.15f, currentPosition.y, currentPosition.z));
                break;
        }
    }
    private void TapToStart()
    {
        state = State.Run;
        playerAnimator.PlayRunAnimation();
    }

    private void SwipeRight()
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
    private void SwipeLeft()
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
    private void SwipeUp()
    {
        state = State.Jump;
    }


    private void Run()
    {
        //Move the GameObject
        rb.velocity = new Vector3(0, -RunSpeed * 10, RunSpeed * 20);
        currentVelocity = rb.velocity;
    }
    private void Jump()
    {
        currentPosition.y = transform.position.y;
        while(transform.position.y <= (currentVelocity.y + 5.5))
            rb.velocity = new Vector3(0, RunSpeed * 1, RunSpeed * 5);



    }

    private void Fall()
    {

    }
}
