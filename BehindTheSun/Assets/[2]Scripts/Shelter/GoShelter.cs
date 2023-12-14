using UnityEngine;
using UnityEngine.SceneManagement;

public class GoShelter : MonoBehaviour
{
    private CharacterController2D Player;
    private bool isGoTrigger = false;

    private void Start()
    {
        Player = FindObjectOfType<CharacterController2D>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)&& isGoTrigger == true) {
            Player.isOut = false;
            SceneManager.LoadScene("Shelter");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") {
            isGoTrigger = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") {
            isGoTrigger= false;

        }
    }

}
