using UnityEngine;
using UnityEngine.SceneManagement;

public class GoShelter : MonoBehaviour
{
    private CharacterController2D Player;

    private void Start()
    {
        Player = FindObjectOfType<CharacterController2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dummy" ) {
                SceneManager.LoadScene("Shelter");
            
        }
    }
}
