using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // ¿Ãµø ∏  ¿Ã∏ß
    private CharacterController2D playerController;
    private CameraFollow cameraFollow;

    private void Start()
    {
        playerController = FindObjectOfType<CharacterController2D>();
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dummy") {
            playerController.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }
    }
}
