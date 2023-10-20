using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint; // �� �̵�, �÷��̾� ���� ��ġ
    private CharacterController2D playerController;
    private CameraFollow cameraFollow;

    void Start()
    {
        playerController = FindObjectOfType<CharacterController2D>();
        cameraFollow = FindObjectOfType<CameraFollow>();

        if (startPoint == playerController.currentMapName) {
            cameraFollow.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -12);
            playerController.transform.position = this.transform.position;
        }
    }
}
