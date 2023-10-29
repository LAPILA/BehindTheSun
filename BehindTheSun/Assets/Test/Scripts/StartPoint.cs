using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint; // 맵 이동, 플레이어 시작 위치
    private CharacterController2D playerController;
    private CameraFollow cameraFollow;

    void Start()
    {
        playerController = FindObjectOfType<CharacterController2D>();
        cameraFollow = FindObjectOfType<CameraFollow>();

        if (startPoint == playerController.currentMapName) {
            cameraFollow.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            playerController.transform.position = new Vector3(playerController.destinationX, playerController.destinationY, transform.position.z + 1);
        }
    }
}
