using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float startX; // ���� x ��ǥ
    public float endX;   // �� x ��ǥ
    public float speed;  // �̵� �ӵ�


    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime; // x ��ǥ�� �������� speed��ŭ �̵�

        // �� x ��ǥ�� �����ϸ� ���� x ��ǥ�� ����
        if (transform.position.x <= endX) {
            transform.position = new Vector2(startX, transform.position.y);
        }
    }
}
