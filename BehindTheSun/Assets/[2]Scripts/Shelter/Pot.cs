using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    bool isInteractable = false;
    bool isGrwoing = false; //감자가 자라는 중인지 획인
    int potato = 0;
    int mypotato = 0;
    [SerializeField] Sprite NonePotImage = null;
    [SerializeField] Sprite PotatoPotImage = null;
    //씨앗 아이템 필요시 변수 추가 필요, 농사 창에 삽입된 씨앗 수.
    //int seed = 다른  함수의 seed 수 
    void Start()
    {
        NonePotImage = GetComponent<Sprite>();
    }

    // Update is called once per frame
    void Update()
    {
        //씨앗 아이템 필요 시, 조건문 추가필요
        if (isInteractable  && Input.GetKeyDown(KeyCode.C) && !isGrwoing) {
            //seed>0일때만 생성 
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
        mypotato += potato;
        potato = 0;
        Debug.Log($"Potato: {potato}");
        Debug.Log($"MyPotato: {mypotato}");
    }

    private IEnumerator GrowingPotato()
    {
        isGrwoing = true;
        //씨앗 필요하면, 씨앗 감소 함수 seed-- 를 아이템 DB에 업뎃;
        Debug.Log("씨앗 확인 완료, 농사 시작");
        yield return new WaitForSeconds(2f);
        PotatoPotImage = GetComponent<Sprite>();
        Debug.Log("수확완료");
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
