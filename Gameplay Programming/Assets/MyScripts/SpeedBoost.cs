﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private PlayerController play;
    public GameObject player;

    private void Awake()
    {
        play = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Collider PlayerCollider = player.GetComponent<Collider>();
        if(other == PlayerCollider)
        {
            play.speed = 45;
        }
    }
}
