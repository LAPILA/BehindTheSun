using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    GameObject GetAlarm = null;
    SpriteRenderer spriteRenderer;
    bool isInteractable = false;
    bool isGrwoing = false; //���ڰ� �ڶ�� ������ ȹ��
    int potato = 0;
    int mypotato = 0;
    [SerializeField] Sprite NonePotImage = null;
    [SerializeField] Sprite PotatoPotImage = null;
    //���� ������ �ʿ�� ���� �߰� �ʿ�, ��� â�� ���Ե� ���� ��.
    //int seed = �ٸ�  �Լ��� seed �� 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetAlarm = transform.GetChild(0).gameObject;
        GetAlarm.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //���� ������ �ʿ� ��, ���ǹ� �߰��ʿ�
        if (isInteractable  && Input.GetKeyDown(KeyCode.C) && !isGrwoing) {
            //seed>0�϶��� ���� 
            if (potato == 0) {
                StartCoroutine(GrowingPotato());
            }
            if(potato != 0) {
                Harvest();
            }
        }
    }

    private void Harvest()
    {
        GetAlarm.SetActive(false);
        spriteRenderer.sprite = NonePotImage;
        mypotato += potato;
        potato = 0;
        Debug.Log($"Potato: {potato}");
        Debug.Log($"MyPotato: {mypotato}");
    }

    private IEnumerator GrowingPotato()
    {
        isGrwoing = true;
        //���� �ʿ��ϸ�, ���� ���� �Լ� seed-- �� ������ DB�� ����;
        Debug.Log("���� Ȯ�� �Ϸ�, ��� ����");
        yield return new WaitForSeconds(2f);
        spriteRenderer.sprite = PotatoPotImage;
        GetAlarm.SetActive(true);
        Debug.Log("��Ȯ�Ϸ�");
        potato += 8;
        isGrwoing = false;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInteractable = false;
    }
}
