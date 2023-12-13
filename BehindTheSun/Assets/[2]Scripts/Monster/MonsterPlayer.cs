using System.Collections;
using UnityEngine;
public class MonsterPlayer : MonoBehaviour
{
    public float moveSpeed = 2f; // ���� ������ �ӵ�
    public float patrolRange = 5f; // ���� ����
    public Transform groundDetection; // �� ������ ����Ʈ
    public Transform groundDetection2; // �� ������ ����Ʈ2
    public LayerMask groundLayer; // �� ���̾�
    public LayerMask playerLayer; // �÷��̾� ���̾�

    private bool movingRight = true; // ó���� �������� ������
    public GunShoot gunShooter;

    private void Start()
    {
    }
    void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D groundInfo2 = Physics2D.Raycast(groundDetection2.position, Vector2.right, 0.1f, groundLayer);

        if (!groundInfo.collider) {
            Flip();
        }

        if(groundInfo2.collider) {
            Flip();
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null) {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= patrolRange) {
                StopMoving();
                LookAtPlayer(player.transform.position);
                gunShooter.AimAtPlayer(player.transform.position);
                gunShooter.Shoot();
            }

            else {
                ResumeMoving();
                Patrol();
            }
        }
        else {
            ResumeMoving();
            Patrol();
        }


    }


    // �¿� ����
    void Flip()
    {
        movingRight = !movingRight;
        Vector3 flippedScale = transform.localScale;
        flippedScale.x *= -1;
        transform.localScale = flippedScale;
    }

    // �̵� ����
    void StopMoving()
    {
        moveSpeed = 0f; // ���߰� ��
    }

    // �̵� �簳
    void ResumeMoving()
    {
        moveSpeed = 2f; // �ٽ� �̵�
    }

    // ���� �̵�
    void Patrol()
    {
        Vector2 moveDirection = movingRight ? Vector2.right : Vector2.left;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    // �÷��̾ �ٶ󺸰� ��
    void LookAtPlayer(Vector3 playerPosition)
    {
        transform.eulerAngles = playerPosition.x > transform.position.x ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
    }

}
