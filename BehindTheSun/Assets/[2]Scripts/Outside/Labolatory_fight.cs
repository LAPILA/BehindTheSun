using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Labolatory_fight : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�
    public float destinationX; // ���� ���� x ��ǥ
    public float destinationY; // ���� ���� y ��ǥ
    private CharacterController2D playerController;
    private CameraFollow cameraFollow;
    private gamemanager GM;
    public int monster_num = 5;

    private void Start()
    {
        GM = FindObjectOfType<gamemanager>();
        playerController = FindObjectOfType<CharacterController2D>();
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    private void Update()
    {
        GM = FindObjectOfType<gamemanager>();
        playerController = FindObjectOfType<CharacterController2D>();
        cameraFollow = FindObjectOfType<CameraFollow>();

        if (monster_num <= 0)
        {
            GM.fightEnd = true;
            playerController.currentMapName = transferMapName;
            Debug.Log(playerController.currentMapName);
            playerController.destinationX = destinationX;
            playerController.destinationY = destinationY;
            playerController.Scene_moves = true;
            SceneManager.LoadScene(transferMapName);
        }
    }
}
