using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    static public CharacterController2D instance;
    private float horizontal;
    public float speed = 9f;
    public float jumpingPower = 50f;
    private bool isFacingRight = true;
    private bool isRun = false;
    public bool isControl;
    private Animator animator;
    public string currentMapName;
    SpriteRenderer spriteRenderer;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
        animator = GetComponent<Animator>();
        isControl = true;
        
    }
    void Update()
    {

        if (isControl) {

            horizontal = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Jump") && IsGrounded()) {
                Debug.Log("����");
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f) {
                Debug.Log("����");
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            // ���� ó�� (Z Ű)
            if (Input.GetKeyDown(KeyCode.Z)) {
                Debug.Log("Z����");
                // ���� ���� ����
                // ���� ���� �ڵ带 �߰�
            }

            // ���� ó�� (Esc Ű)
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Debug.Log("ESC����");
                // ���� â�� ���ų� ���� �Ͻ� ����
                // ���� ó�� �ڵ带 �߰�
            }

            // ��ȣ�ۿ� ó�� (C Ű)
            if (Input.GetKeyDown(KeyCode.C)) {
                Debug.Log("C��ȣ�ۿ�");
                // ��ȣ�ۿ� ���� ����
                // ���� �ڵ带 �߰�
            }

            // isRunning ���� ����
            if (Mathf.Abs(horizontal) > 0) {
                isRun = true;
            }
            else {
                isRun = false;
            }

            // "Run" �ִϸ��̼��� ����
            animator.SetBool("isRun", isRun);


            Flip();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // rb.velocity.y�� ���⼭ �������� ����
    }

    private bool IsGrounded()
    {
        Debug.Log("Grounded");
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            OnDamaged(collision.transform.position);
    }
    private void OnDamaged(Vector2 targetPos) // �ǰ�
    {
        // �÷��̾� ���� ���̾� �̵�
        gameObject.layer = 11;

        // �ǰݽ� ������ �ϰ�
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // ƨ�ܳ�����
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirc,100), ForceMode2D.Impulse);
    }
}
