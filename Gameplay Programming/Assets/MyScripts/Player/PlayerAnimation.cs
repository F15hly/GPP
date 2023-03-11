using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMovement playerMovement;
    InputManager inputManager;
    private Animator anim;
    private HashIDs hash;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        inputManager = GetComponent<InputManager>();
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();

        anim.SetLayerWeight(1, 1f);
    }

    private void Update()
    {
        if (inputManager.xInp != 0 || inputManager.yInp != 0)
        {
            anim.SetFloat(hash.walkSpeed, 1);
        }
        if (inputManager.xInp == 0 && inputManager.yInp == 0)
        {
            anim.SetFloat(hash.walkSpeed, 0);
        }

        if(playerMovement.isFalling && playerMovement.inAirTimer >=0)
        {
            anim.SetBool(hash.falling, true);
        }
        if(!playerMovement.isFalling)
        {
            anim.SetBool(hash.falling, false);
        }

        if(playerMovement.isJump)
        {
            anim.SetBool(hash.jump, true);
        }
        if (!playerMovement.isJump)
        {
            anim.SetBool(hash.jump, false);
        }

        if(!playerMovement.flip && playerMovement.jumpUpgradeCollected)
        {
            anim.SetBool(hash.flip, true);
        }
        if (playerMovement.flip && playerMovement.inAirTimer >= 0)
        {
            anim.SetBool(hash.flip, false);
        }
    }
}
