using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
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

    public LayerMask groundLayer; // Ground ���̾ üũ�ϱ� ���� LayerMask
    public Transform groundCheck; // Ground üũ�� ������Ʈ

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
    //���ľ��ҵ�
    void ChasePlayer()
    {
        transform.Find("WatchingIcon").gameObject.SetActive(true);
        isChasing = true;

        if (isGrounded) {//����
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (isGrounded && !isAttacking) {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange) {
                Attack();
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        // �÷��̾� ������ ���� ��ȯ
        if (transform.position.x < player.position.x) {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // �÷��̾��� �����ʿ� ������ �������� ������ ����
        }
        else {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f); // �÷��̾��� ���ʿ� ������ ������ ������ ����
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
}