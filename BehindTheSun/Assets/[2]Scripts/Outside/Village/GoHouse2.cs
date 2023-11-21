using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHouse2 : MonoBehaviour
{
    private CharacterController2D Player;

    private void Start()
    {
        Player = FindObjectOfType<CharacterController2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dummy" && Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("House2");

        }
    }
}