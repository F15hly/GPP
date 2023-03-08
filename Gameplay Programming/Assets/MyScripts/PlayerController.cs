using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float testFloat = 0;

    private Rigidbody rb;
    public float movementX;
    public float movementY;
    public float speed = 15f;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 5f;
    private float jumpTime = 0;

    public Transform camTarget;
    public Transform playerBody;

    public GameObject jumpUpgrade;
    public bool isDoubleJump = false;
    public bool jumpUpgradeCollected = false;

    private Animator anim;
    private HashIDs hash;
    bool fall = false;

    public bool TwoD = false;

    public GameObject camContainer;
    public GameObject twoDcam;
    public GameObject Camera;
    public Transform twoDcamContainer;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();

        anim.SetLayerWeight(1, 1f);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Collider JumpCollider = jumpUpgrade.GetComponent<Collider>();
        if (other == JumpCollider)
        {
            jumpUpgradeCollected = true;
            //Destroy(jumpUpgrade);
        }
    }
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
        if(TwoD)
        { 
            if(movementX < 0)
            {
                movementY = 1;
                playerBody.localRotation = twoDcamContainer.rotation;
                playerBody.Rotate(0f, 0f, 0f);
            }
            if(movementX > 0)
            {
                movementY = 1;
                playerBody.localRotation = twoDcamContainer.rotation;
                playerBody.Rotate(0f, 180f, 0f);
            }
        }
        if (!TwoD)
        {
            if (movementY != 0)
            {
                playerBody.rotation = camTarget.rotation;
                camTarget.rotation = playerBody.rotation;
            }
            if (movementX > 0)
            {
                movementY = 1;
                playerBody.localRotation = camTarget.rotation;
                playerBody.Rotate(0f, 90f, 0f);
            }
            if (movementX < 0)
            {
                movementY = 1;
                playerBody.localRotation = camTarget.rotation;
                playerBody.Rotate(0f, -90f, 0f);
            }
            if (movementY < 0)
            {
                playerBody.localRotation = camTarget.rotation;
                playerBody.Rotate(0f, 180f, 0f);
                movementY = 1;
            }
        }

    }
    private void OnPunch(InputValue input)
    {
        anim.SetBool(hash.punch, true);
    }
    private void OnJump(InputValue input)
    {
        anim.SetBool(hash.jump, true);
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if(!isGrounded && isDoubleJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -5f * gravity);
            anim.SetBool(hash.flip, true);
            isDoubleJump = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 move = transform.forward * movementY;

        controller.Move(move * speed * Time.deltaTime);

        anim.SetBool(hash.punch, false);
    }

     void Update()
    {
        if (TwoD)
        {
            camContainer.GetComponent<MyCamera>().enabled = false;
            twoDcam.SetActive(true);
            Camera.SetActive(false);
            twoDcamContainer.transform.localPosition = playerBody.transform.position;
        }
        if(!TwoD)
        {
            camContainer.GetComponent<MyCamera>().enabled = true;
            twoDcam.SetActive(false);
            Camera.SetActive(true);
            camTarget.transform.localPosition = playerBody.transform.position;
        }

        if (speed > 15)
        {
            speed -= Time.deltaTime * 15;
        }

        testFloat = movementY;

        ///jump stuff
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(!isGrounded)
        {
            jumpTime += Time.deltaTime * 2;
        }
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -5f;
            jumpTime = 1.1f;
            anim.SetBool(hash.jump, false);
        }
        if(jumpUpgradeCollected && isGrounded && velocity.y < 0)
        {
            isDoubleJump = true;
            fall = false;
            anim.SetBool(hash.flip, false);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(velocity.y < gravity)
        {
            fall = true;
        }

        //animations
        anim.SetFloat(hash.walkSpeed, movementY);
        anim.SetFloat(hash.jumpLength, Mathf.Pow(jumpTime, -1));
        anim.SetBool(hash.grounded, isGrounded);
        anim.SetBool(hash.falling, fall);
    }
}
