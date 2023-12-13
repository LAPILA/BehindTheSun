using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float startX; // 시작 x 좌표
    public float endX;   // 끝 x 좌표
    public float speed;  // 이동 속도


    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime; // x 좌표를 왼쪽으로 speed만큼 이동

        // 끝 x 좌표에 도달하면 시작 x 좌표로 변경
        if (transform.position.x <= endX) {
            transform.position = new Vector2(startX, transform.position.y);
        }
    }
}
