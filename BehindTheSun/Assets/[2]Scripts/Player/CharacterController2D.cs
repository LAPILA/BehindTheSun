using UnityEditorInternal; // ������ ���� Ŭ���� ���
using UnityEngine; // ����Ƽ ���� Ŭ���� ���
using UnityEngine.SceneManagement; // �� ������ ���� ���ӽ����̽� ���

public class CharacterController2D : MonoBehaviour
{
    public static CharacterController2D instance; // �̱��� ������ ���� �ν��Ͻ�
    public gamemanager gameManager;
    SpriteRenderer spriteRenderer;
    private const float GroundCheckRadius = 0.2f; // ���� Ȯ���� ���� ������
    public GameObject menu;

    public bool isOut = true;

    private float horizontal; // ���� �̵��� ���� ����
    public float speed = 9f; // �̵� �ӵ�
    public float jumpingPower = 15f; // ���� ��

    private bool isFacingRight = true; // ĳ���Ͱ� �������� ���� �ִ��� ����
    private bool isRun = false; // ĳ���Ͱ� �޸��� �ִ��� ����
    private bool isJumping = false;
    public bool isControl; // ��Ʈ�� ������ �������� ����


    private Animator animator; // �ִϸ����� ������Ʈ ����
    public string currentMapName;
    public float destinationX = 0; // ���� ���� x ��ǥ
    public float destinationY = 0; // ���� ���� y ��ǥ
    public bool Scene_moves = false;

    [SerializeField] private Rigidbody2D rb; // ���� ���� ������ ���� Rigidbody2D ����
    [SerializeField] private Transform groundCheck; // ���� Ȯ���� ���� Transform ����
    [SerializeField] private LayerMask groundLayer; // ���� ���̾�

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // �̱��� ���� ����
        if (instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }

        // �ִϸ����� ������Ʈ ��������
        animator = GetComponent<Animator>();

        // ��Ʈ�� ���� ���·� �ʱ�ȭ
        isControl = true;
    }

    void Update()
    {
        if (Scene_moves)
        {
            Player_scene_move();
            Scene_moves = false;
        }
        
        if (isControl) {
            // �̵�, ����, �׼� ó��
            HandleMovement();
            HandleJumping();
            

            // ĳ���� ���� ��ȯ
            Flip();
        }
        //HandleActions(); esc��ư ������ �� �޴�â ���� ���� �������� ���� ��� �ּ�ó�� ��
    }

    private void FixedUpdate()
    {
        // ���� ĳ���� �̵� ó��
        MoveCharacter();
    }

    private void HandleMovement()
    {
        // ���� �̵� ó��
        horizontal = Input.GetAxisRaw("Horizontal");

        // �޸��� ���� ������Ʈ
        isRun = Mathf.Abs(horizontal) > 0;
        animator.SetBool("isRun", isRun);
    }

    private void HandleJumping()
    {
        // ���� ó��
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            isJumping = true;
            animator.SetBool("isJump", isJumping);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        // ���� ó��
        if (IsGrounded()) {
            isJumping = false;
            animator.SetBool("isJump", isJumping);
        }
    }

    //public void HandleActions()
    //{
        
    //    if (Input.GetButtonDown("Cancel"))
    //    {
    //        //Debug.Log("ESC����");
    //        if (menu.activeSelf)
    //        {
    //            menu.SetActive(false);
    //        }
    //        else
    //        {
    //            menu.SetActive(true);
    //        }
    //        Debug.Log("ESC����");
    //    }
    //}
    

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bullet")
        {

            gameManager.Normal_Wolf_Damaged();
            OnDamaged(collision.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "Bullet")
        {
            Debug.Log("�ѿ� �´�");
            gameManager.Normal_Bullet_Damaged();
            OnDamaged(collision.transform.position);
        }
        if( collision.gameObject.name == "Boss_Attack")
        {
            gameManager.Boss_Damaged();
            OnDamaged(collision.transform.position);
        }
    }

    public void OnDamaged(Vector2 targetPos)
    {   
        //
        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);


        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirc, 1)*5,ForceMode2D.Impulse);

        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    private void MoveCharacter()
    {
        // Rigidbody�� �̿��� ĳ���� �̵�
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        // ���� Ȯ��
        Debug.Log("Grounded");
        return Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, groundLayer);
    }

    private void Flip()
    {
        // ĳ���� ���� ��ȯ
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void Player_scene_move()
    {
        transform.position = new Vector3(destinationX, destinationY, transform.position.z);
    }
}
