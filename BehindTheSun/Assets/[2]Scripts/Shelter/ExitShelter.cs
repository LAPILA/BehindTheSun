using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitShelter : MonoBehaviour
{
    private CharacterController2D Player;
    private bool isGoTrigger = false;
    private Vector2 nextScenePosition = new Vector2(6.12f, -1.48f);

    private void Start()
    {
        Player = FindObjectOfType<CharacterController2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && isGoTrigger == true) {
            Player.destinationX = 5.5f;
            Player.destinationY = -2;
            Player.Scene_moves = true;
            SceneManager.LoadScene("Mine");
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
            isGoTrigger = false;

        }
    }

}
