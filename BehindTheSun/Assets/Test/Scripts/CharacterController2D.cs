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
                Debug.Log("점프");
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f) {
                Debug.Log("점프");
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            // 공격 처리 (Z 키)
            if (Input.GetKeyDown(KeyCode.Z)) {
                Debug.Log("Z공격");
                // 공격 동작 실행
                // 공격 동작 코드를 추가
            }

            // 설정 처리 (Esc 키)
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Debug.Log("ESC설정");
                // 설정 창을 열거나 게임 일시 정지
                // 설정 처리 코드를 추가
            }

            // 상호작용 처리 (C 키)
            if (Input.GetKeyDown(KeyCode.C)) {
                Debug.Log("C상호작용");
                // 상호작용 동작 실행
                // 동작 코드를 추가
            }

            // isRunning 변수 설정
            if (Mathf.Abs(horizontal) > 0) {
                isRun = true;
            }
            else {
                isRun = false;
            }

            // "Run" 애니메이션을 실행
            animator.SetBool("isRun", isRun);


            Flip();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // rb.velocity.y는 여기서 변경하지 않음
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
    private void OnDamaged(Vector2 targetPos) // 피격
    {
        // 플레이어 무적 레이어 이동
        gameObject.layer = 11;

        // 피격시 판투명 하게
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // 튕겨나가기
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirc,100), ForceMode2D.Impulse);
    }
}
