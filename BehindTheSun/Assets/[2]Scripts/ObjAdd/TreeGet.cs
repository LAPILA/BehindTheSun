using UnityEngine;
using System.Collections;

public class TreeGet : MonoBehaviour
{
    bool treeEnter = false;
    bool isShaking = false; // ����ũ ���� üũ
    int tree_count = 3;
    int RandomTree = 0;
    int attackCount = 0;

    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!isShaking && treeEnter && Input.GetKeyDown(KeyCode.C)) {
            if (attackCount < 3) {
                attackCount++;
            }
            else {
                RandomTree = Random.Range(4, 10); // ������ ���� ���� ����
                Resource.Instance.AddResource("����", RandomTree);
                StartCoroutine(ShakeObject(initialPosition, 0.1f, 0.05f, 0.05f, 0.2f));
                attackCount = 0;
                tree_count--;
            }
        }

        if(tree_count <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            treeEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            treeEnter = false;
        }
    }

    // ������ ���� �Լ�
    IEnumerator ShakeObject(Vector3 initialPos, float shakeDuration, float shakeAmountX, float shakeAmountY, float shakeInterval)
    {
        isShaking = true; // ����ũ ������ ǥ��

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration) {
            Vector3 newPos = initialPos + new Vector3(Random.Range(-shakeAmountX, shakeAmountX), Random.Range(-shakeAmountY, shakeAmountY), 0);
            transform.position = newPos;
            elapsedTime += shakeInterval;
            yield return new WaitForSecondsRealtime(shakeInterval); // ����Ÿ������ ���
        }

        transform.position = initialPos;
        yield return new WaitForSeconds(0.3f);

        isShaking = false; // ����ũ ����
    }
}
