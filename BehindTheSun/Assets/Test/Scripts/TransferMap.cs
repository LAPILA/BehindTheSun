using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; //�̵� �� �̸�
    private CharacterController2D Player;
    private CameraFollow Camera;

    private void Start()
    {
        Player = FindObjectOfType<CharacterController2D>();
        Camera = FindObjectOfType<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="dummy") {
            Player.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);

        }
    }
}
