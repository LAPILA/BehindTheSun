using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
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
        if (GenGage) {
            animator.SetBool("OnSys", true);
        }
        else {
            animator.SetBool("OnSys", false);
        }
        if(GenGage&&isInteractable&& Input.GetKeyDown(KeyCode.C)) {
            //�ɾƼ� TV���� PlayerAnimation
            //���ŷ� ȸ�� ��ƾ
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
