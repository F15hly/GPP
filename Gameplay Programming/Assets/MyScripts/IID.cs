using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IID : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject mainCam;
    public GameObject IIDCam;

    public Transform IIDCamTransform;
    public Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        Collider playerCollider = playerObj.GetComponent<Collider>();
        if (other == playerCollider)
        {
            IIDCamTransform.rotation = transform.rotation;
            mainCam.SetActive(false);
            IIDCam.SetActive(true);
        }
    }
    private void Update()
    {
        IIDCamTransform.position = playerTransform.position;
    }
}
