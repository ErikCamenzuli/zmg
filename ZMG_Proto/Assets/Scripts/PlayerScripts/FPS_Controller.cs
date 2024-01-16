using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Controller : MonoBehaviour
{
    //Referencing the camera on Player
    public Transform cameraTransform;
    //Referencing character controller on Player capsual
    public CharacterController characterController;

    //Player Settings Variables
    public float camSens = 0;
    public float playerMoveSpeed;
    public float inputDeadZone;

    //Player Movement
    private Vector2 moveStartingPosition;
    private Vector2 moveInput;

    //Phone touching detection
    private int leftHandFinger, rightHandFinger;
    private float screenHalfWidth;

    //Camera control
    private Vector2 lookingInput;
    private float camPitch;

    // Start is called before the first frame update
    void Start()
    {
        //These are acting as IDs for fingers. -1 means that the finger(s) are not being tracked
        leftHandFinger = -1;
        rightHandFinger = -1;
        
        //We only need to find out the screen size once, so we do it in the Start() function
        screenHalfWidth = Screen.width / 2;

        //Figuring out movement input dead zone
        inputDeadZone = Mathf.Pow(Screen.height / inputDeadZone, 2);
    }

    // Update is called once per frame
    void Update()
    {
        TouchInput();

        if(rightHandFinger != -1)
        {
            LookAround();
            Debug.Log("Rotation Achieved");
        }

        if (leftHandFinger != -1)
        {
            PlayerMovement();
            Debug.Log("Movement Achieved");
        }
    }


    void TouchInput()
    {
        //Interating through all the touching inputs
        for (int i =0; i< Input.touchCount;i++)
        {
            Touch t = Input.GetTouch(i);

            //checking touches
            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x < screenHalfWidth && leftHandFinger ==-1)
                    {
                        leftHandFinger = t.fingerId;

                        moveStartingPosition = t.position;
                    }
                    else if (t.position.x > screenHalfWidth && rightHandFinger == -1)
                    {
                        rightHandFinger = t.fingerId;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if(t.fingerId == leftHandFinger)
                    {
                        leftHandFinger = -1;
                        Debug.Log("Left hand Finger tracking Stopped");
                    }
                    else if(t.fingerId == rightHandFinger)
                    {
                        rightHandFinger = -1;
                        Debug.Log("Right hand Finger tracking Stopped");
                    }
                    break;
                case TouchPhase.Moved:
                    if(t.fingerId == rightHandFinger)
                    {
                        lookingInput = t.deltaPosition * camSens * Time.deltaTime;
                    }
                    else if (t.fingerId == leftHandFinger)
                    {
                        moveInput = t.position - moveStartingPosition;
                    }
                    break;
                case TouchPhase.Stationary:
                    if(t.fingerId == rightHandFinger)
                    {
                        lookingInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void PlayerMovement()
    {
        //if (moveInput.sqrMagnitude <= inputDeadZone)
            //return;
        Vector2 moveDir = moveInput.normalized * playerMoveSpeed * Time.deltaTime;
        characterController.Move(transform.right * moveDir.x + transform.forward * moveDir.y);
    }

    void LookAround()
    {
        //pitch rotation
        camPitch = Mathf.Clamp(camPitch - lookingInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(camPitch, 0, 0);
        //horizontal rotation
        transform.Rotate(transform.up, lookingInput.x);
    }
}
