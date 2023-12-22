using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannedGet : MonoBehaviour
{
    bool RefrigeratorEnter = false;
    int RandomCanned = 0;
    int Canned_count = 3;

    private ToolBar tB;

    private void Start()
    {
        tB = FindObjectOfType<ToolBar>();
    }
    void Update()
    {
        if (RefrigeratorEnter && Input.GetKeyDown(KeyCode.C))
        {
            RandomCanned = Random.Range(1, 4);
            tB.Canned_Food_quantity += RandomCanned;
            Canned_count--;
        }

        if(Canned_count <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RefrigeratorEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RefrigeratorEnter = false;
        }
    }
}
