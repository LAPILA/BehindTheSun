using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    static public CameraFollow instance;
    public LayerMask targetLayer; // Player 레이어를 가진 오브젝트를 추적하기 위한 레이어 마스크

    private Transform target;
    public float smoothSpeed = 3;
    public Vector2 offset;
    public float limitMinX, limitMaxX, limitMinY, limitMaxY;
    float cameraHalfWidth, cameraHalfHeight;

    private void Start()
    {
        if (instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        FindTarget(); // 매 LateUpdate마다 타겟을 찾아서 추적

        if (target != null) {
            Vector3 desiredPosition = new Vector3(
                Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X
                transform.position.y, // 이제 Y는 고정
                transform.position.z); // Z 위치는 현재와 동일

            // 카메라의 위치를 부드럽게 이동
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }
    }

    // 씬 전환 후에도 Player 레이어 또는 태그를 가진 오브젝트를 추적
    private void FindTarget()
    {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Player"); // Player 태그를 가진 모든 오브젝트를 찾음

        foreach (GameObject possibleTarget in possibleTargets) {
            if (((1 << possibleTarget.layer) & targetLayer) != 0) {
                target = possibleTarget.transform;
                break;
            }
        }
    }

    // 새로 추가된 함수: 카메라의 위치를 명시적으로 설정
    public void SetCameraPosition(Vector3 newPosition)
    {
        Vector3 cameraPos = new Vector3(newPosition.x, transform.position.y, transform.position.z); // Y와 Z 위치는 현재와 동일
        transform.position = cameraPos;
    }
}
