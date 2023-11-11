using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YongGwang : MonoBehaviour
{
    private bool isInteractable = false;
    public bool GenGage = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isInteractable && GenGage && Input.GetKeyDown(KeyCode.C)) {
            animator.SetBool("OnSys", true);
            StartCoroutine(RefineResources());
            
        }
    }

    private IEnumerator RefineResources()
    {
        if (Resource.Instance.GetResourceQuantity("석탄") > 0) {
            // 석탄 소모
            Resource.Instance.RemoveResource("석탄", 1);

            //UI가 아직 마련되지 않아 각각 10개씩 또는 최소한으로 추가하는 기능으로 만들었습니다.
            //추후 수정 필요

            yield return new WaitForSeconds(5f); // 제련 시간 5초 대기
            // 철 제련
            int ironCount = Resource.Instance.GetResourceQuantity("철");
            int refineIron = Mathf.Min(10, ironCount);
            if (ironCount >= refineIron) {
                Resource.Instance.RemoveResource("철", refineIron);
                Resource.Instance.AddResource("제련된 철", refineIron);
            }

            // 금 제련
            int goldCount = Resource.Instance.GetResourceQuantity("금");
            int refineGold = Mathf.Min(10, goldCount);
            if (goldCount >= refineGold) {
                Resource.Instance.RemoveResource("금", refineGold);
                Resource.Instance.AddResource("제련된 금", refineGold);
            }


            Debug.Log("제련 완료: 제련된 철 " + refineIron + "개, 제련된 금 " + refineGold + "개");
            animator.SetBool("OnSys", false);
        }
        else {
            Debug.Log("석탄이 부족하여 제련할 수 없습니다.");
        }
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
