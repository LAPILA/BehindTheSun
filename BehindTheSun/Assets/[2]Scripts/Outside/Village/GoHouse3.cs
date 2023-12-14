using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHouse3 : MonoBehaviour
{
    private CharacterController2D Player;
    private bool isGoTrigger = false;

    private void Start()
    {
        Player = FindObjectOfType<CharacterController2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && isGoTrigger == true)
        {
            SceneManager.LoadScene("House3");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dummy")
        {
            isGoTrigger = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dummy")
        {
            isGoTrigger = false;

        }
    }
}