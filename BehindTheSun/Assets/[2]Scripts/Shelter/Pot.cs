using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Pot : MonoBehaviour
{
    ToolBar toolBar;
    GameObject GetAlarm = null;
    SpriteRenderer spriteRenderer;
    bool isInteractable = false;
    bool isGrwoing = false; //���ڰ� �ڶ�� ������ ȹ��
    int potato = 0;
    [SerializeField] Sprite NonePotImage = null;
    [SerializeField] Sprite PotatoPotImage = null;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetAlarm = transform.GetChild(0).gameObject;
        GetAlarm.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteractable  && Input.GetKeyDown(KeyCode.C) && !isGrwoing) {
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
        toolBar.Potato_quantity += potato;
        potato = 0;
    }

    private IEnumerator GrowingPotato()
    {
        isGrwoing = true;
        //���� �ʿ��ϸ�, ���� ���� �Լ� seed-- �� ������ DB�� ����;
        Debug.Log("���� Ȯ�� �Ϸ�, ��� ����");
        yield return new WaitForSeconds(1920f);
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
