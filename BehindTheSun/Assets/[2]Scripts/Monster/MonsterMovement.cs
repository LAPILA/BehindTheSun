using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public ToolBar TB;
    public gamemanager gameManager;
    int Monster_HP = 40;

    public Transform player;
    public float moveSpeed = 8f;
    public float leftBound = -5f;
    public float rightBound = 5f;
    public float patrolSpeed = 3f;
    public float minPatrolDelay = 1f;
    public float maxPatrolDelay = 3f;
    public float jumpForce = 12f;
    public float attackRange = 5f;
    public float attackSpeed = 15f;

    bool isPlayerNearby = false;
    Rigidbody2D rb;
    bool isJumping = false;
    bool isChasing = false;
    bool isGrounded = false;
    bool isAttacking = false;

    public LayerMask groundLayer; // Ground 레이어를 체크하기 위한 LayerMask
    public Transform groundCheck; // Ground 체크용 오브젝트

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.Find("WatchingIcon").gameObject.SetActive(false);

        InvokeRepeating(nameof(Patrol), 0f, Random.Range(minPatrolDelay, maxPatrolDelay));
        InvokeRepeating(nameof(CheckGround), 0f, 0.1f);
        Invoke(nameof(ChangePatrolDirection), Random.Range(minPatrolDelay, maxPatrolDelay));
    }

    void Update()
    {
        FindAndAssignInstances();
        if (player != null) {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < 15f) {
                isPlayerNearby = true;
            }
            else {
                isPlayerNearby = false;
            }

            if (isPlayerNearby) {

                ChasePlayer();
            }
        }
    }
    //고쳐야할듯
    private void FindAndAssignInstances()
    {
        if (TB == null) {
            ToolBar foundDayTimer = FindObjectOfType<ToolBar>();
            if (foundDayTimer != null) {
                TB = foundDayTimer;
            }
        }

        if (gameManager == null) {
            gamemanager foundGameManager = FindObjectOfType<gamemanager>();
            if (foundGameManager != null) {
                gameManager = foundGameManager;
            }
        }
    }
    void ChasePlayer()
    {
        transform.Find("WatchingIcon").gameObject.SetActive(true);
        isChasing = true;

        if (isGrounded) {//점프
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (isGrounded && !isAttacking) {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange) {
                Attack();
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        // 플레이어 쪽으로 방향 전환
        if (transform.position.x < player.position.x) {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // 플레이어의 오른쪽에 있으면 오른쪽을 보도록 설정
        }
        else {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f); // 플레이어의 왼쪽에 있으면 왼쪽을 보도록 설정
        }
    }

    void Attack()
    {
        isAttacking = true;
        Vector2 attackDirection = (player.position - transform.position).normalized;

        rb.velocity = attackDirection * attackSpeed;

        Invoke(nameof(StopAttack), 1.5f);
    }

    void StopAttack()
    {
        isAttacking = false;
        rb.velocity = Vector2.zero;
        transform.localScale = new Vector3(Mathf.Sign(player.position.x - transform.position.x) * 0.5f, 0.5f, 0.5f);
    }

    void Patrol()
    {
        transform.Find("WatchingIcon").gameObject.SetActive(false);
        if (!isChasing) {
            if (Random.Range(0, 2) == 0) {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(rightBound, transform.position.y), patrolSpeed * Time.deltaTime);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(leftBound, transform.position.y), patrolSpeed * Time.deltaTime);
                transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            }
        }
        isChasing = false;
    }

    void ChangePatrolDirection()
    {
        isChasing = false;
        if (!isPlayerNearby) {
            if (isJumping) {
                rb.velocity = new Vector2(rb.velocity.x * -1, 0);
            }
            else {
                if (Random.Range(0, 2) == 0) {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(rightBound, transform.position.y), patrolSpeed * Time.deltaTime);
                    transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                else {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(leftBound, transform.position.y), patrolSpeed * Time.deltaTime);
                    transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
                }
            }
        }
        Invoke(nameof(ChangePatrolDirection), Random.Range(minPatrolDelay, maxPatrolDelay));
        
    }

    void ResetJump()
    {
        isJumping = false;
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            // 피스톨
            if (gameManager.pistol_use)
            {
                Debug.Log("피스톨을 맞췄다");
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

        if (Monster_HP <= 0)
        {
            TB.Meet_quantity += 3;

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
