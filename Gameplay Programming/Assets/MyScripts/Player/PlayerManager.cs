using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    CameraManger cameraManger;
    PlayerMovement playerMovement;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManger = FindObjectOfType<CameraManger>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        inputManager.HandleAllInputs();
    }
    private void FixedUpdate()
    {
        playerMovement.HandleAllMovement();
    }
    private void LateUpdate()
    {
        cameraManger.handleAllCameraMovement();
    }
}
