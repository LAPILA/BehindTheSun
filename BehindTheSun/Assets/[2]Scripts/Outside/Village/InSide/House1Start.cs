using UnityEngine;
using UnityEngine.SceneManagement;

public class House1Start : MonoBehaviour
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
            SceneManager.LoadScene("Village");
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
