using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject door;
    public GameObject player;
    private PlayerController play;

    private void Awake()
    {
        play = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider PlayerCollider = player.GetComponent<Collider>();
        if(other == PlayerCollider)
        {
            door.GetComponent<doorOpen>().enabled = true;
            play.movementY = 0f;
        }
    }
}
