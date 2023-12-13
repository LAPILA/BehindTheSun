using UnityEditorInternal; // 에디터 내부 클래스 사용
using UnityEngine; // 유니티 엔진 클래스 사용
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스 사용

public class CharacterController2D : MonoBehaviour
{
    public static CharacterController2D instance; // 싱글톤 패턴을 위한 인스턴스
    private const float GroundCheckRadius = 0.2f; // 지면 확인을 위한 반지름

    private float horizontal; // 수평 이동을 위한 변수
    public float speed = 9f; // 이동 속도
    public float jumpingPower = 15f; // 점프 힘

    private bool isFacingRight = true; // 캐릭터가 오른쪽을 보고 있는지 여부
    private bool isRun = false; // 캐릭터가 달리고 있는지 여부
    private bool isJumping = false;
    public bool isControl; // 컨트롤 가능한 상태인지 여부


    private Animator animator; // 애니메이터 컴포넌트 참조
    public string currentMapName;
    public float destinationX = 0; // 시작 지점 x 좌표
    public float destinationY = 0; // 시작 지점 y 좌표

    [SerializeField] private Rigidbody2D rb; // 물리 엔진 접근을 위한 Rigidbody2D 참조
    [SerializeField] private Transform groundCheck; // 지면 확인을 위한 Transform 참조
    [SerializeField] private LayerMask groundLayer; // 지면 레이어

    private void Start()
    {
        // 싱글톤 패턴 구현
        if (instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }

        // 애니메이터 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        // 컨트롤 가능 상태로 초기화
        isControl = true;
    }

    void Update()
    {
        
        if (isControl) {
            // 이동, 점프, 액션 처리
            HandleMovement();
            HandleJumping();

            // 캐릭터 방향 전환
            Flip();
        }
    }

    private void FixedUpdate()
    {
        // 실제 캐릭터 이동 처리
        MoveCharacter();
    }

    private void HandleMovement()
    {
        // 수평 이동 처리
        horizontal = Input.GetAxisRaw("Horizontal");

        // 달리기 상태 업데이트
        isRun = Mathf.Abs(horizontal) > 0;
        animator.SetBool("isRun", isRun);
    }

    private void HandleJumping()
    {
        // 점프 처리
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            isJumping = true;
            animator.SetBool("isJump", isJumping);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        // 착지 처리
        if (IsGrounded()) {
            isJumping = false;
            animator.SetBool("isJump", isJumping);
        }
    }


    private void MoveCharacter()
    {
        // Rigidbody를 이용한 캐릭터 이동
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        // 지면 확인
        Debug.Log("Grounded");
        return Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, groundLayer);
    }

    private void Flip()
    {
        // 캐릭터 방향 전환
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
