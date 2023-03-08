using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{ 
    public GameObject playerObj;
    public Transform playerTransform;
    private PlayerController play;

    public float test;

    private void Awake()
    {
        play = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void OnTriggerEnter(Collider other)
    {
        Collider playerCollider = playerObj.GetComponent<Collider>();
        if (other = playerCollider)
        {
            if (play.TwoD)
            {
                play.twoDcamContainer.localRotation = transform.rotation;
                if (play.movementX < 0)
                {
                    play.playerBody.localRotation = play.twoDcamContainer.rotation;
                }
                if (play.movementX > 0)
                {
                    play.playerBody.localRotation = play.twoDcamContainer.rotation;
                    play.playerBody.Rotate(0, 180, 0);
                }
            }
        }
    }
}
