using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class boss : MonoBehaviour
{
    public Transform player;
    public GameObject Boss_Attack;
    Animator animator;
    int BossHp = 1000;
    gamemanager gameManager;
    private CharacterController2D playerController;
    bool isWalking = false;
    bool isAttacking = false;
    bool isFacingRight = true; // To track the boss's direction
    bool player_in_range = false;

    float walkSpeed = 50f;

    void Start()
    {
        Boss_Attack.SetActive(false);
        playerController = FindObjectOfType<CharacterController2D>();
        gameManager = FindObjectOfType<gamemanager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        StartCoroutine(BossBehaviour());
    }

    IEnumerator BossBehaviour()
    {
        while (BossHp > 0) {
            float Playerdistance = Vector2.Distance(player.position, transform.position);
            //데미지 받으면
            //Damage();
            if (Playerdistance > 30) {
                CastSpellAndAttack();
            }
            else if (Playerdistance <= 7) {
                Attack();
            }
            else if (Playerdistance > 7 && Playerdistance < 30) {
                MoveTowardsPlayer();
            }

            yield return new WaitForSeconds(0.1f);
        }

        Death();
    }

    void CastSpellAndAttack()
    {
        if (!isAttacking) {
            isWalking = false;
            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isAttacking", false);

            animator.SetTrigger("isCast");
            StartCoroutine(TeleportAndSpell());
        }
    }

    IEnumerator TeleportAndSpell()
    {
        animator.SetTrigger("isCast");
        yield return new WaitForSeconds(1f); 

        Vector3 teleportPos = new Vector3(player.position.x - 8.0f, player.position.y + 3.0f, player.position.z);
        transform.position = teleportPos;
    }


    void MoveTowardsPlayer()
    {
        if (!isAttacking) {
            isWalking = true;
            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isAttacking", false);

            Vector3 targetPosition = new Vector3(player.position.x, player.position.y + 3.0f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, walkSpeed * Time.deltaTime);

            // Flip the boss's direction based on player position
            if (player.position.x > transform.position.x && !isFacingRight) {
                Flip();
            }
            else if (player.position.x < transform.position.x && isFacingRight) {
                Flip();
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Attack()
    {
        if (!isAttacking) {
            isWalking = false;
            isAttacking = true;
            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isAttacking", isAttacking);
            float aw = Time.deltaTime;
            Invoke("Active_Attack_collider", 0.4f);

            //데미지 코드 넣으시면 될듯
            Invoke("ResetAttack", 1f);
        }
    }

    void Active_Attack_collider()
    {
        Boss_Attack.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player_Bullets")
        {
            // 피스톨
            if (gameManager.pistol_use)
            {
                Debug.Log("피스톨을 맞췄다");
                gameManager.pistol_use = false;
                Pistol_Damaged();
                Damage();
            }
            // 총2
            if (gameManager.shotgun_use)
            {
                gameManager.shotgun_use = false;
                Shotgun_Damaged();
                Damage();
            }
            // 총3
            if (gameManager.rifle_use)
            {
                gameManager.rifle_use = false;
                Rifle_Damaged();
                Damage();
            }
        }

        if (BossHp <= 0)
        {
            Death();
        }
    }


    void Pistol_Damaged()
    {
        BossHp -= 25;
        //피스톨에 데미지 입는다
    }

    void Shotgun_Damaged()
    {
        BossHp -= 50;
    }

    void Rifle_Damaged()
    {
        BossHp -= 40;
    }

    void Damage()
    {
        animator.SetTrigger("isHurt");
    }
    void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        Boss_Attack.SetActive(false);
    }

    void Death()
    {
        isWalking = false;
        isAttacking = false;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetTrigger("isDeath");
        Destroy(gameObject);
        if (gameManager.hidden_ending_open)
        {
            playerController.destinationX = 44;
            playerController.destinationY = -3;
            playerController.Scene_moves = true;
            SceneManager.LoadScene("Hidden_Ending");
        }
        else
        {
            playerController.destinationX = 44;
            playerController.destinationY = -3;
            playerController.Scene_moves = true;
            SceneManager.LoadScene("Test_Scri_Boss");
        }
    }
}
