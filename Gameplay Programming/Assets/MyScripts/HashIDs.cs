using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int walkSpeed;
    public int jump;
    public int jumpLength;
    public int flip;
    public int grounded;
    public int falling;
    public int punch;

    private void Awake()
    {
        walkSpeed = Animator.StringToHash("Walk");
        jump = Animator.StringToHash("Jump");
        jumpLength = Animator.StringToHash("JumpLength");
        flip = Animator.StringToHash("Flip");
        grounded = Animator.StringToHash("Grounded");
        falling = Animator.StringToHash("Falling");
        punch = Animator.StringToHash("Punch");
    }
 }
