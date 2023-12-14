using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // ÀÌµ¿ÇÒ ¸ÊÀÇ ÀÌ¸§
    public float destinationX; // µµÂø ÁöÁ¡ x ÁÂÇ¥
    public float destinationY; // µµÂø ÁöÁ¡ y ÁÂÇ¥
    private CharacterController2D playerController;
    private CameraFollow cameraFollow;

    private void Start()
    {
        playerController = FindObjectOfType<CharacterController2D>();
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") {
            playerController.currentMapName = transferMapName;
            playerController.destinationX = destinationX;
            playerController.destinationY = destinationY;
            SceneManager.LoadScene(transferMapName);
        }
    }
}
