using UnityEngine;
using System.Collections;

public class boss : MonoBehaviour
{
    public Transform player;
    Animator animator;
    int BossHp = 1000;
    bool isAttacking = false;

    float walkSpeed = 1.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(BossBehaviour());
    }

    IEnumerator BossBehaviour()
    {
        while (BossHp > 0) {
            float Playerdistance = Vector3.Distance(player.position, transform.position);

            if (Playerdistance > 20) {
                CastSpell();
            }
            else if (Playerdistance <= 20 && Playerdistance > 2) {
                MoveTowardsPlayer();
            }
            else if (Playerdistance <= 2 && !isAttacking) {
                Attack();
            }

            yield return new WaitForSeconds(0.1f);
        }

        Death();
    }

    void CastSpell()
    {
        if (!isAttacking) {
            animator.SetTrigger("isCast");
            Invoke("TeleportAndSpell", 1f);
        }
    }

    void TeleportAndSpell()
    {
        Vector3 teleportPos = new Vector3(player.position.x, player.position.y + 5f, player.position.z);
        transform.position = teleportPos;
        animator.SetTrigger("isSpell");
        StartCoroutine(AttackAfterDelay(0.5f));
    }

    IEnumerator AttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Attack();
    }

    void MoveTowardsPlayer()
    {
        animator.SetFloat("Speed", walkSpeed);
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, walkSpeed * Time.deltaTime);
    }

    void Attack()
    {
        if (!isAttacking) {
            isAttacking = true;
            animator.SetFloat("Speed", 0f);
            animator.SetTrigger("isAttack");
            Invoke("ResetAttack", 1f);
        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }


    void Death()
    {
        animator.SetTrigger("isDeath");
        Destroy(gameObject);
    }
}
