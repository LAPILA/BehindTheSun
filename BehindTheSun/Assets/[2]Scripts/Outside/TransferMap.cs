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
        playerController = FindObjectOfType<CharacterController2D>();
        cameraFollow = FindObjectOfType<CameraFollow>();

        if (collision.gameObject.tag == "Player") {
            Debug.Log("1123¾ÀÀÌµ¿ ÀÛµ¿");
            playerController.currentMapName = transferMapName;
            Debug.Log(playerController.currentMapName);
            playerController.destinationX = destinationX;
            playerController.destinationY = destinationY;
            playerController.Scene_moves = true;
            SceneManager.LoadScene(transferMapName);
        }
    }
}
