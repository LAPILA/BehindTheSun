using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public static Generator Instance;
    private bool isInteractable = false; // ��ȣ�ۿ� ������ ���¸� ��Ÿ���� ����
    private int coalCount = 0; // �����⿡ �ִ� ��ź�� ��
    private Coroutine coalConsumptionCoroutine; // ��ź �Һ� �ڷ�ƾ
    public Drill drill;
    public YongGwang yongGwang;
    public TV tv;
    private Animator animator;
    GameObject GEN_UI = null;

    private int coalToAdd = 0; 
    public TMP_Text remainText; 
    public TMP_Text AddText;
    public Button Up;
    public Button Down;
    public Button Push;

    private void Start()
    {
        GEN_UI = transform.GetChild(0).gameObject;
        if (GEN_UI != null) {
            GEN_UI.SetActive(false);
        }
        Instance = this;
        animator = GetComponent<Animator>();
        if(animator == null) {
            Debug.Log("����");
        }
        Up.onClick.AddListener(IncreaseCoalToAdd);
        Down.onClick.AddListener(DecreaseCoalToAdd);
        Push.onClick.AddListener(PushCoal);

    }

    void Update()
    {

        //��ģ���¿��� CŰ ������ �۵�
        if (Input.GetKeyDown(KeyCode.C) && isInteractable) {
            GenUIOn();
        }
    }

    void GenUIOn()
    {
        if (GEN_UI != null) {
            GEN_UI.SetActive(!GEN_UI.activeSelf);
            remainText.text = "USING " + coalCount;
            AddText.text = "ADD " + coalToAdd;
        }
    }



    void UseGenerator()
    {
        EnsureDependenciesInitialized();
        //�ڿ�â���� ��ź, ������ŭ ����
        if (coalToAdd > 0 && Resource.Instance.RemoveResource("��ź", (int)coalToAdd)) {
            coalCount += coalToAdd; // ���� ��ź �߰�
            Debug.Log("��ź�� �߰��߽��ϴ�. ���� ��ź ��: " + coalCount);
            remainText.text = "USING " + coalCount; 

            //��ź�� 0�ʰ��� ��ź �Һ� �ڷ�ƾ�� ������¶��
            if (coalConsumptionCoroutine == null && coalCount > 0) {
                //�Һ� �ڷ�ƾ ����
                coalConsumptionCoroutine = StartCoroutine(ConsumeCoal());
                //�ִϸ��̼� ����
                animator.SetBool("OnSys", true);
            }
        }
        else if (coalToAdd <= 0) {
            Debug.Log("�߰��� ��ź�� �����ϴ�.");
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
    void IncreaseCoalToAdd()
    {
        coalToAdd++;
        UpdateCoalToAddText();
    }

    void DecreaseCoalToAdd()
    {
        if (coalToAdd > 0) coalToAdd--;
        UpdateCoalToAddText();
    }

    public void PushCoal()
    {
        UseGenerator();
        coalToAdd = 0; 
        UpdateCoalToAddText();
    }

    private void UpdateCoalToAddText()
    {
        //���� �ߺ� ������ �ʿ䰡? 
        AddText.text = "ADD " + coalToAdd;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInteractable = false;
        if (GEN_UI != null) {
            GEN_UI.SetActive(false);
        }
    }
}
