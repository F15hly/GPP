using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour
{
    public Transform camTarget;
    public Transform camOrigin;
    public Transform door;

    public GameObject button;
    public GameObject cameraController;
    public GameObject thingThatOpens;
    public GameObject player;

    public ParticleSystem confetti;

    private float speed = 0f;
    public bool zoomIn = false;
    public bool zoomOut = false;
    public bool inCutScene = false;

    private void Awake()
    {
        zoomIn = true;
        inCutScene = true;
    }
    void Update()
    {
        if(zoomIn)
        {
            cameraController.GetComponent<MyCamera>().enabled = false;
            player.GetComponent<PlayerController>().enabled = false;

            camTarget.LookAt(door);
            speed = 10f;
            var step = speed * Time.deltaTime;
            camTarget.position = Vector3.MoveTowards(camTarget.position, door.position, step);
            if (Vector3.Distance(camTarget.position, door.position) < 15)
            {
                zoomIn = false;
                StartCoroutine(Cutscene());  
            }
        }
        if(zoomOut)
        {
            speed = 10f;
            var step = speed * Time.deltaTime;
            camTarget.position = Vector3.MoveTowards(camTarget.position, camOrigin.position, step);
            if (Vector3.Distance(camTarget.position,camOrigin.position) < 1)
            {
                zoomIn = false;
                zoomOut = false;
                inCutScene = false;
            }
        }
        if(!zoomIn &! zoomOut &! inCutScene)
        {
            cameraController.GetComponent<MyCamera>().enabled = true;
            player.GetComponent<PlayerController>().enabled = true;
            camTarget.rotation = camOrigin.rotation;
        }
    }
    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(1);
        GetComponent<AudioSource>().pitch = 1f;
        GetComponent<AudioSource>().Play();
        Destroy(thingThatOpens);
        ///play pop sound
        yield return new WaitForSeconds(2);
        confetti.GetComponent<AudioSource>().pitch = 1f;
        confetti.GetComponent<AudioSource>().Play();
        confetti.Play();
        ///play fireworks sound
        yield return new WaitForSeconds(3);
        zoomOut = true;
        button.GetComponent<Button>().enabled = true;
    }
}
