using System.Collections;
using UnityEngine;
public class MonsterPlayer : MonoBehaviour
{
    public ToolBar TB;
    public gamemanager gameManager;
    int Monster_HP = 50;

    public float moveSpeed = 2f; // 몬스터 움직임 속도
    public float patrolRange = 5f; // 정찰 범위
    public Transform groundDetection; // 땅 감지용 포인트
    public Transform groundDetection2; // 땅 감지용 포인트2
    public LayerMask groundLayer; // 땅 레이어
    public LayerMask playerLayer; // 플레이어 레이어

    private bool movingRight = true; // 처음엔 우측으로 움직임
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

 
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            // 피스톨
            if (gameManager.pistol_use)
            {
                Debug.Log("피스톨을 맞았다");
                gameManager.pistol_use = false;
                Pistol_Damaged();
                
            }
            // 총2
            if (gameManager.shotgun_use)
            {
                gameManager.shotgun_use = false;
                Shotgun_Damaged();
            }
            // 총3
            if (gameManager.rifle_use)
            {
                gameManager.rifle_use = false;
                Rifle_Damaged();
            }
        }

        if(Monster_HP <= 0)
        {
            int drop_random = Random.Range(1, 11);

            if(drop_random <= 5)
            {
                TB.Pistol_bullet += 12;
            }else if(drop_random <= 8)
            {
                TB.Rifle_bullet += 12;
            }
            else
            {
                TB.Shotgun_bullet += 12;
            }

            Destroy(gameObject);
        }
        // 체력 0 이하가 되면 디스트로이
    }

    void Pistol_Damaged()
    {
        Monster_HP -= 25;
        //피스톨에 데미지 입는다
    }

    void Shotgun_Damaged()
    {
        Monster_HP -= 50;
    }

    void Rifle_Damaged()
    {
        Monster_HP -= 40;
    }

}
