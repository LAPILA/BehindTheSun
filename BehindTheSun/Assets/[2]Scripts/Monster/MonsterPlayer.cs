using System.Collections;
using UnityEngine;
public class MonsterPlayer : MonoBehaviour
{
    public ToolBar TB;
    public gamemanager gameManager;
    int Monster_HP = 50;

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

 
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            // �ǽ���
            if (gameManager.pistol_use)
            {
                Debug.Log("�ǽ����� �¾Ҵ�");
                gameManager.pistol_use = false;
                Pistol_Damaged();
                
            }
            // ��2
            if (gameManager.shotgun_use)
            {
                gameManager.shotgun_use = false;
                Shotgun_Damaged();
            }
            // ��3
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
        // ü�� 0 ���ϰ� �Ǹ� ��Ʈ����
    }

    void Pistol_Damaged()
    {
        Monster_HP -= 25;
        //�ǽ��翡 ������ �Դ´�
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
