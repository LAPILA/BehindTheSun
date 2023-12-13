using UnityEditorInternal; // ������ ���� Ŭ���� ���
using UnityEngine; // ����Ƽ ���� Ŭ���� ���
using UnityEngine.SceneManagement; // �� ������ ���� ���ӽ����̽� ���

public class CharacterController2D : MonoBehaviour
{
    public static CharacterController2D instance; // �̱��� ������ ���� �ν��Ͻ�
    private const float GroundCheckRadius = 0.2f; // ���� Ȯ���� ���� ������

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

    [SerializeField] private Rigidbody2D rb; // ���� ���� ������ ���� Rigidbody2D ����
    [SerializeField] private Transform groundCheck; // ���� Ȯ���� ���� Transform ����
    [SerializeField] private LayerMask groundLayer; // ���� ���̾�

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
        
        if (isControl) {
            // �̵�, ����, �׼� ó��
            HandleMovement();
            HandleJumping();

            // ĳ���� ���� ��ȯ
            Flip();
        }
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
}
