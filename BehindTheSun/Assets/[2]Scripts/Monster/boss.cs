using UnityEngine;
using System.Collections;

public class boss : MonoBehaviour
{
    public Transform player;
    Animator animator;
    int BossHp = 1000;
    bool isWalking = false;
    bool isAttacking = false;
    bool isFacingRight = true; // To track the boss's direction

    float walkSpeed = 50f;

    void Start()
    {
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
            else if (Playerdistance <= 8) {
                Attack();
            }
            else if (Playerdistance > 8 && Playerdistance < 30) {
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

        Vector3 teleportPos = new Vector3(player.position.x, player.position.y+1, player.position.z);
        transform.position = teleportPos;
    }


    void MoveTowardsPlayer()
    {
        if (!isAttacking) {
            isWalking = true;
            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isAttacking", false);

            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
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

            //데미지 코드 넣으시면 될듯.
            Invoke("ResetAttack", 1f);
        }
    }

    void Damage()
    {
        animator.SetTrigger("isHurt");
    }
    void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }

    void Death()
    {
        isWalking = false;
        isAttacking = false;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetTrigger("isDeath");
        Destroy(gameObject);
    }
}
