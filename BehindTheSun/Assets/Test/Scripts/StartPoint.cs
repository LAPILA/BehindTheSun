using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint; //맵이동, 플레이어 시작위치
    private CharacterController2D Player;
    private CameraFollow Camera;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<CharacterController2D>();
        Camera = FindObjectOfType<CameraFollow>();

        if (startPoint == Player.currentMapName) {
            Camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -12);
            Player.transform.position = this.transform.position;
        }
    }

}
