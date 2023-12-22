using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�
    public float destinationX; // ���� ���� x ��ǥ
    public float destinationY; // ���� ���� y ��ǥ
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
            Debug.Log("1123���̵� �۵�");
            playerController.currentMapName = transferMapName;
            Debug.Log(playerController.currentMapName);
            playerController.destinationX = destinationX;
            playerController.destinationY = destinationY;
            playerController.Scene_moves = true;
            SceneManager.LoadScene(transferMapName);
        }
    }
}
