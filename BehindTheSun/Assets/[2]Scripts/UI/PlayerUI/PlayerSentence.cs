using System.Collections;
using UnityEngine;

public class PlayerSentence : MonoBehaviour
{
    private bool isTalking = false; // ��ȭ ������ ���θ� ��Ÿ���� ���� �߰�
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
        isTalking = true; // ��ȭ ���� �� ��ȭ �� ���·� ����

        GameObject existingChatBox = GameObject.FindGameObjectWithTag("ChatBox"); // �̹� �����ϴ� ��ȭâ Ȯ��

        if (existingChatBox == null) // ��ȭâ�� ���ٸ� ���ο� ��ȭâ ����
        {
            GameObject go = Instantiate(chatBoxPrefab);
            go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
            StartCoroutine(WaitForDialogueEnd(go)); // ��ȭ�� ����� ������ ���
        }
        else
        {
            // �̹� ��ȭâ�� �������� ��쿡 ���� ó���� ���ϸ� ���⿡ �߰�
            // ���� ���, �̹� ���� ��ȭâ�� �ݰų� �ٸ� ó���� ������ �� �ֽ��ϴ�.
        }
    }

    IEnumerator WaitForDialogueEnd(GameObject chatBox)
    {
        yield return new WaitUntil(() => chatBox == null); // ��ȭâ�� �ı��� ������ ���
        isTalking = false; // ��ȭ ���� �� ��ȭ �� ���¸� false�� ����
    }
}



