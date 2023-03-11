using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;

    public Vector3 moveDirection;
    Transform camObj;
    Transform IIDcamObj;
    public Rigidbody rb;

    public float speed = 10f;
    public float sens = 15;

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundDistance;

    public float inAirTimer;
    public float gravity;
    public bool isFalling;

    public bool isJump;
    public float jumpHeight = 3;

    public bool jumpUpgradeCollected;
    public GameObject jumpUpgrade;
    public bool flip;

    public bool onPlatform;

    public bool IID = false;

    public float test;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        camObj = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        Falling();
        Movement();
        Rotation();
    }

    private void Movement()
    {
        moveDirection = camObj.forward * inputManager.yInp;
        moveDirection = moveDirection + camObj.right * inputManager.xInp;
        moveDirection.Normalize();
        moveDirection.y = 0;
        
        moveDirection = moveDirection * speed;

        Vector3 move = moveDirection;

        rb.velocity = move;
    }

    private void Rotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = camObj.forward * inputManager.yInp;
        targetDirection = targetDirection + camObj.right * inputManager.xInp;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, sens * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void Falling()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;

        if(!isGrounded)
        {
            isFalling = true;

            inAirTimer = inAirTimer + Time.deltaTime;
            rb.AddForce(-Vector3.up * gravity * inAirTimer * 60);
        }
        if ((Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer)&& !isJump)
            || (onPlatform && !isJump))
        {
            inAirTimer = 0;
        }
            
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer) || onPlatform)
        {
            if(!isGrounded)
            {
                isFalling = false;
            }
            isGrounded = true;
            flip = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void HandleJump()
    {
        if(isGrounded || onPlatform)
        {
            isJump = true;
            isGrounded = false;
            inAirTimer = -jumpHeight;
            test = 1;
        }
        if(isFalling && flip)
        {
            inAirTimer = -jumpHeight;
            flip = false;
        }
    }

    private void Update()
    {
        //jump
        if (inAirTimer >= 0)
        {
            isJump = false;
        }
        if(!jumpUpgradeCollected)
        {
            flip = false;
        }
        if(isGrounded && isJump)
        {
            isGrounded = false;
        }

        //speed
        if (speed > 15)
        {
            speed -= Time.deltaTime * 15;
        }
        if (speed < 15)
        {
            speed += Time.deltaTime * 15;
        }
    }
}
