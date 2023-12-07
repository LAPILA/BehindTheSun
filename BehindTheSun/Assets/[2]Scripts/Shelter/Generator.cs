using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static Generator Instance;
    public int coalConsumptionRate = 80; // ��ź �Ҹ��� , ���� ���� �����ۿ��� ���� ��ź ���� ���� �뵵�� ���� �ʿ�
    private bool isInteractable = false; // ��ȣ�ۿ� ������ ���¸� ��Ÿ���� ����
    private int coalCount = 0; // �����⿡ �ִ� ��ź�� ��
    private Coroutine coalConsumptionCoroutine; // ��ź �Һ� �ڷ�ƾ
    public Drill drill;
    public YongGwang yongGwang;
    public TV tv;
    private Animator animator;
    private void Start()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        if(animator == null) {
            Debug.Log("����");
        }

    }

    void Update()
    {

        //��ģ���¿��� CŰ ������ �۵�
        if (Input.GetKeyDown(KeyCode.C) && isInteractable) {
            UseGenerator();
        }
    }

    void UseGenerator()
    {
        EnsureDependenciesInitialized();
        //�ڿ�â���� ��ź, ������ŭ ����
        if (Resource.Instance.RemoveResource("��ź", (int)coalConsumptionRate)) {
            coalCount += coalConsumptionRate; // ���� ��ź �߰�
            Debug.Log("��ź�� �߰��߽��ϴ�. ���� ��ź ��: " + coalCount);

            //��ź�� 0�ʰ��� ��ź �Һ� �ڷ�ƾ�� ������¶��
            if (coalConsumptionCoroutine == null && coalCount > 0) {
                //�Һ� �ڷ�ƾ ����
                coalConsumptionCoroutine = StartCoroutine(ConsumeCoal());
                //�ִϸ��̼� ����
                animator.SetBool("OnSys", true);
            }
        }
        else {
            Debug.Log("��ź�� �����Ͽ� �����⸦ �۵���ų �� �����ϴ�.");
        }
    }

    IEnumerator ConsumeCoal()
    {
        while (true) {
            // ��ź �Һ�
            coalCount--;
            Debug.Log("��ź�� ����߽��ϴ�. ���� ��ź ��: " + coalCount);

            // ��ź�� 0�� �Ǹ� �ݺ� ����
            if (coalCount <= 0) {
                Debug.Log("��ź�� ���� �����Ⱑ �����Ǿ����ϴ�.");
                StopGenerator();
                break; // ���� ����
            }

            EnsureDependenciesInitialized();
            ActivateMachines();


            // 12�� ���
            yield return new WaitForSeconds(12f);
        }
    }

    private void EnsureDependenciesInitialized()
    {
        if (drill == null)
            drill = FindObjectOfType<Drill>();
        if (yongGwang == null)
            yongGwang = FindObjectOfType<YongGwang>();
        if (tv == null)
            tv = FindObjectOfType<TV>();
    }
    private void ActivateMachines()
    {
        drill.GenGage = true;
        yongGwang.GenGage = true;
        tv.GenGage = true;
    }

    private void StopGenerator()
    {
        animator.SetBool("OnSys", false);
        drill.GenGage = false;
        yongGwang.GenGage = false;
        tv.GenGage = false;
        coalConsumptionCoroutine = null;
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
