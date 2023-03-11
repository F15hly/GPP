using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private PlayerMovement play;
    public GameObject player;

    private void Awake()
    {
        play = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Collider PlayerCollider = player.GetComponent<CapsuleCollider>();
        if(other == PlayerCollider)
        {
            play.speed = 45;
        }
    }
}
