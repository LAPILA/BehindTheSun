using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditorInternal; // ������ ���� Ŭ���� ���
using UnityEngine.SceneManagement; // �� ������ ���� ���ӽ����̽� ���

public class ESC_UI : MonoBehaviour
{
    
    public GameObject menu;
    public static ESC_UI instance; // �̱��� ������ ���� �ν��Ͻ�
    private const float GroundCheckRadius = 0.2f; // ���� Ȯ���� ���� ������

    private float horizontal; // ���� �̵��� ���� ����
    public float speed = 9f; // �̵� �ӵ�
    public float jumpingPower = 15f; // ���� ��

    //private bool isFacingRight = true; // ĳ���Ͱ� �������� ���� �ִ��� ����
    //private bool isRun = false; // ĳ���Ͱ� �޸��� �ִ��� ����
    public bool isControl; // ��Ʈ�� ������ �������� ����

    //private Animator animator; // �ִϸ����� ������Ʈ ����
    public string currentMapName;
    public float destinationX = 0; // ���� ���� x ��ǥ
    public float destinationY = 0; // ���� ���� y ��ǥ

    //[SerializeField] private Rigidbody2D rb; // ���� ���� ������ ���� Rigidbody2D ����
    //[SerializeField] private Transform groundCheck; // ���� Ȯ���� ���� Transform ����
    //[SerializeField] private LayerMask groundLayer; // ���� ���̾�

    private void Start()
    {
        // �̱��� ���� ����
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        // �ִϸ����� ������Ʈ ��������
        //animator = GetComponent<Animator>();

        // ��Ʈ�� ���� ���·� �ʱ�ȭ
        isControl = true;
    }

    void Update()
    {
        if (isControl)
        {
            // �̵�, ����, �׼� ó��
            //HandleMovement();
            //HandleJumping();
            HandleActions();

            // ĳ���� ���� ��ȯ
            //Flip();
        }
    }

    //private void FixedUpdate()
    //{
    //    // ���� ĳ���� �̵� ó��
    //    MoveCharacter();
    //}

    //private void HandleMovement()
    //{
    //    // ���� �̵� ó��
    //    horizontal = Input.GetAxisRaw("Horizontal");

    //    // �޸��� ���� ������Ʈ
    //    isRun = Mathf.Abs(horizontal) > 0;
    //    animator.SetBool("isRun", isRun);
    //}

    //private void HandleJumping()
    //{
    //    // ���� ó��
    //    if (Input.GetButtonDown("Jump") && IsGrounded())
    //    {
    //        Debug.Log("����");
    //        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    //    }
    //}

    private void HandleActions()
    {
        
        if (Input.GetButtonDown("Cancel"))
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
            Debug.Log("ESC����");
            // ���� ���� �߰� ����
        }
    }

    public void GameExit()
    {
        Application.Quit();
    }

    //private void MoveCharacter()
    //{
    //    // Rigidbody�� �̿��� ĳ���� �̵�
    //    rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    //}

    //private bool IsGrounded()
    //{
    //    // ���� Ȯ��
    //    Debug.Log("Grounded");
    //    return Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, groundLayer);
    //}

    //private void Flip()
    //{
    //    // ĳ���� ���� ��ȯ
    //    if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
    //    {
    //        isFacingRight = !isFacingRight;
    //        Vector3 localScale = transform.localScale;
    //        localScale.x *= -1f;
    //        transform.localScale = localScale;
    //    }
    //}
    
}




