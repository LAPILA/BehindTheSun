using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shelter_Transfer : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�
    public float destinationX; // ���� ���� x ��ǥ
    public float destinationY; // ���� ���� y ��ǥ
    private CharacterController2D playerController;
    private CameraFollow cameraFollow;
    private gamemanager GM;

    private void Start()
    {
        playerController = FindObjectOfType<CharacterController2D>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        GM = FindObjectOfType<gamemanager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GM = FindObjectOfType<gamemanager>();

        if (GM.laboratoy_open)
        {
            if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.C))
            {
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
}
