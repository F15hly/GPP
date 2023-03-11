using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public GameObject player;
    PlayerMovement playerMovement;
    public float speed = 5f;
    public Transform switchPoint1;
    public Transform switchPoint2;
    public Transform switchPoint3;
    public Transform switchPoint4;

    public int currentTarget = 1;
    public int totalTagets;
    private float bugTimer = 0;
    private bool bugBool = false;

    Vector3 move;

    private Transform Target;

    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
        if(other == player)
        {
            playerMovement.onPlatform = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
    void FixedUpdate()
    {
        if (currentTarget == 1)
        {
            Target = switchPoint1;
        }
        if (currentTarget == 2)
        {
            Target = switchPoint2;
        }
        if (currentTarget == 3)
        {
            Target = switchPoint3;
        }
        if (currentTarget == 4)
        {
            Target = switchPoint4;
        }

        if(currentTarget > totalTagets)
        {
            currentTarget = 1;
            bugBool = true;
        }
        if (bugBool == true)
        {
            bugTimer += Time.deltaTime;
        }
        if(bugTimer > 1)
        {
            bugBool = false;
            bugTimer = 0;
        }

        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target.position, step);
        if (Vector3.Distance(transform.position, Target.position) < 1 && bugBool == false)
        {
            currentTarget ++;
        }
    }
}
