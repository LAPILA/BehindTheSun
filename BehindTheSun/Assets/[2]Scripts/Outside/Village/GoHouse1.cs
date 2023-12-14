using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHouse1 : MonoBehaviour
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
            SceneManager.LoadScene("House1");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isGoTrigger = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isGoTrigger = false;

        }
    }
}