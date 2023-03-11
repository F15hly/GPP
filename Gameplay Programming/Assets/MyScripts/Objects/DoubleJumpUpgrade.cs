using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpUpgrade : MonoBehaviour
{
    PlayerMovement playerMovement;
    public GameObject player;
    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponent<CapsuleCollider>())
        {
            playerMovement.jumpUpgradeCollected = true;
            Destroy(gameObject);
        }
    }
}
