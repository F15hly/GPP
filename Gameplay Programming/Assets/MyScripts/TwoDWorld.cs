using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDWorld : MonoBehaviour
{
    public GameObject playerObj;
    public Transform playerTransform;
    private PlayerController play;

    private void Awake()
    {
        play = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
   public void OnTriggerEnter(Collider other)
    {
        Collider playerCollider = playerObj.GetComponent<Collider>();
        if (other = playerCollider)
        {
            play.TwoD = !play.TwoD;
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
