using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGet : MonoBehaviour
{
    bool StoneEnter = false;
    int RandomStone = 0;
    int attackCount = 0;
    void Update()
    {
        if (StoneEnter && Input.GetKeyDown(KeyCode.C)) {
            if (attackCount < 2) {
                attackCount++;
            }
            else {
                RandomStone = Random.Range(1, 5); // 랜덤한 돌 개수 설정
                Resource.Instance.AddResource("돌", RandomStone);
                attackCount = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player") {
            StoneEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player") {
            StoneEnter = false;
        }
    }
}
