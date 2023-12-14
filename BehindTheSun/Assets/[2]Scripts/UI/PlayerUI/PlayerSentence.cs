using System.Collections;
using UnityEngine;

public class PlayerSentence : MonoBehaviour
{
    private bool isTalking = false; // 대화 중인지 여부를 나타내는 변수 추가
    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;
    public bool PlayerIsClose;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isTalking && PlayerIsClose)
        {
            TalkNpc();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = false;
        }
    }

    void TalkNpc()
    {
        isTalking = true; // 대화 시작 시 대화 중 상태로 변경

        GameObject existingChatBox = GameObject.FindGameObjectWithTag("ChatBox"); // 이미 존재하는 대화창 확인

        if (existingChatBox == null) // 대화창이 없다면 새로운 대화창 생성
        {
            GameObject go = Instantiate(chatBoxPrefab);
            go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
            StartCoroutine(WaitForDialogueEnd(go)); // 대화가 종료될 때까지 대기
        }
        else
        {
            // 이미 대화창이 열려있을 경우에 대한 처리를 원하면 여기에 추가
            // 예를 들어, 이미 열린 대화창을 닫거나 다른 처리를 수행할 수 있습니다.
        }
    }

    IEnumerator WaitForDialogueEnd(GameObject chatBox)
    {
        yield return new WaitUntil(() => chatBox == null); // 대화창이 파괴될 때까지 대기
        isTalking = false; // 대화 종료 시 대화 중 상태를 false로 변경
    }
}



