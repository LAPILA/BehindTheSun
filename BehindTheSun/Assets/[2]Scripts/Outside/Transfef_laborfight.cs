using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transfef_laborfight : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�
    public string transferMapName2; // �̵��� ���� �̸�
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController = FindObjectOfType<CharacterController2D>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        GM = FindObjectOfType<gamemanager>();

        if (GM.fightEnd)
        {
            if (collision.gameObject.tag == "Player")
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
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("1123���̵� �۵�");
                playerController.currentMapName = transferMapName2;
                Debug.Log(playerController.currentMapName);
                playerController.destinationX = destinationX;
                playerController.destinationY = destinationY;
                playerController.Scene_moves = true;
                SceneManager.LoadScene(transferMapName2);
            }
        }
    }
}
