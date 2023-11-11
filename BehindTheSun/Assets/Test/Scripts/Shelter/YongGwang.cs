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
        if (Resource.Instance.GetResourceQuantity("��ź") > 0) {
            // ��ź �Ҹ�
            Resource.Instance.RemoveResource("��ź", 1);

            //UI�� ���� ���õ��� �ʾ� ���� 10���� �Ǵ� �ּ������� �߰��ϴ� ������� ��������ϴ�.
            //���� ���� �ʿ�

            yield return new WaitForSeconds(5f); // ���� �ð� 5�� ���
            // ö ����
            int ironCount = Resource.Instance.GetResourceQuantity("ö");
            int refineIron = Mathf.Min(10, ironCount);
            if (ironCount >= refineIron) {
                Resource.Instance.RemoveResource("ö", refineIron);
                Resource.Instance.AddResource("���õ� ö", refineIron);
            }

            // �� ����
            int goldCount = Resource.Instance.GetResourceQuantity("��");
            int refineGold = Mathf.Min(10, goldCount);
            if (goldCount >= refineGold) {
                Resource.Instance.RemoveResource("��", refineGold);
                Resource.Instance.AddResource("���õ� ��", refineGold);
            }


            Debug.Log("���� �Ϸ�: ���õ� ö " + refineIron + "��, ���õ� �� " + refineGold + "��");
            animator.SetBool("OnSys", false);
        }
        else {
            Debug.Log("��ź�� �����Ͽ� ������ �� �����ϴ�.");
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
