using UnityEngine;

public class MonsterPlayer : MonoBehaviour
{
    public float moveSpeed = 2f; // 몬스터 움직임 속도
    public float patrolRange = 5f; // 정찰 범위
    public Transform groundDetection; // 땅 감지용 포인트
    public LayerMask groundLayer; // 땅 레이어
    public LayerMask playerLayer; // 플레이어 레이어

    private bool movingRight = true; // 처음엔 우측으로 움직임
    public GunShoot gunShooter;
    void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.1f, groundLayer);

        if (!groundInfo.collider) {
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


    // 좌우 반전
    void Flip()
    {
        movingRight = !movingRight;
        Vector3 flippedScale = transform.localScale;
        flippedScale.x *= -1;
        transform.localScale = flippedScale;
    }

    // 이동 멈춤
    void StopMoving()
    {
        moveSpeed = 0f; // 멈추게 함
    }

    // 이동 재개
    void ResumeMoving()
    {
        moveSpeed = 2f; // 다시 이동
    }

    // 정찰 이동
    void Patrol()
    {
        Vector2 moveDirection = movingRight ? Vector2.right : Vector2.left;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    // 플레이어를 바라보게 함
    void LookAtPlayer(Vector3 playerPosition)
    {
        transform.eulerAngles = playerPosition.x > transform.position.x ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
    }

}
