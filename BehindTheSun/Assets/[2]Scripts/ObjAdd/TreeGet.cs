using UnityEngine;
using System.Collections;

public class TreeGet : MonoBehaviour
{
    bool treeEnter = false;
    bool isShaking = false; // 쉐이크 여부 체크
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
                RandomTree = Random.Range(4, 10); // 랜덤한 나무 개수 설정
                Resource.Instance.AddResource("나무", RandomTree);
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

    // 수정된 흔들기 함수
    IEnumerator ShakeObject(Vector3 initialPos, float shakeDuration, float shakeAmountX, float shakeAmountY, float shakeInterval)
    {
        isShaking = true; // 쉐이크 중임을 표시

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration) {
            Vector3 newPos = initialPos + new Vector3(Random.Range(-shakeAmountX, shakeAmountX), Random.Range(-shakeAmountY, shakeAmountY), 0);
            transform.position = newPos;
            elapsedTime += shakeInterval;
            yield return new WaitForSecondsRealtime(shakeInterval); // 리얼타임으로 대기
        }

        transform.position = initialPos;
        yield return new WaitForSeconds(0.3f);

        isShaking = false; // 쉐이크 종료
    }
}
