using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class StoneGet : MonoBehaviour
{
    bool StoneEnter = false;
    int RandomStone = 0;
    int attackCount = 0;
    int randomSilver = 0;
    int randomGold = 0;
    int stone_count = 3;
    int randomCoral = 0;

    private ToolBar tB;

    private void Start()
    {
        tB = FindObjectOfType<ToolBar>();
    }
    void Update()
    {
        if (StoneEnter && Input.GetKeyDown(KeyCode.Z) && tB.Get_PickAxe_Active()) {
            if (attackCount < 2) {
                attackCount++;
            }
            else {
                RandomStone = Random.Range(5, 10); // ·£´ýÇÑ µ¹ °³¼ö ¼³Á¤
                randomSilver = Random.Range(2, 6);
                randomGold = Random.Range(2, 6);
                randomCoral = Random.Range(5, 10);
                Resource.Instance.AddResource("µ¹", RandomStone);
                Resource.Instance.AddResource("Ã¶", RandomStone);
                Resource.Instance.AddResource("±Ý", RandomStone);
                Resource.Instance.AddResource("¼®Åº", RandomStone);
                attackCount = 0;
                stone_count--;
            }
        }

        if (stone_count <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            StoneEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            StoneEnter = false;
        }
    }
}
