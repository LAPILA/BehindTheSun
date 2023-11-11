using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    private bool isInteractable = false;//���߿� ��ȣ�ۿ� Ű(������ ȹ�� �߰��ʿ�)
    public bool GenGage = false;//����
    private Coroutine resourceGenerationCoroutine;//�ڿ����� �ڷ�ƾ
    private Dictionary<string, int> temporaryResources = new Dictionary<string, int>();//�帱 �ڿ�, �ϳ��� ���� = �帱 �ڿ� ->cŰ���� �� ->�⺻ �ڿ����� �̵�
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
       
        if(GenGage == true){
            animator.SetBool("OnSys", true);
        }
        if(GenGage == false) {
            animator.SetBool("OnSys", false);
        }
        //���Ⱑ �ְ�, �ڿ� ���� �ڷ�ƾ�� �ߴܵǸ� ����.
        if (GenGage && resourceGenerationCoroutine == null) {
            resourceGenerationCoroutine = StartCoroutine(GenerateResources());

        }
        if (isInteractable && Input.GetKeyDown(KeyCode.C)) {
            GetResource();
        }
    }

  //�Ϸ� ��ü�� 16�и���(1�д�20����) �ڿ� ����
    private IEnumerator GenerateResources()
    {
        for (int i = 0; i < 16; i++) { // 16�� ���� �ݺ�
            yield return new WaitForSeconds(60f); // 1�� ���
            GenerateRandomResources(20); // 1�и��� 20�� �ڿ� ����
        }
        resourceGenerationCoroutine = null;
    }

    //�ڿ� �������� ���� �Լ�
    private void GenerateRandomResources(int count)
    {
        for (int i = 0; i < count; i++) {
            float randomValue = Random.Range(0f, 100f);
            string resourceName = "";
            //�������� ���� �̸��� �����ϰ�,
            if (randomValue <= 40f) resourceName = "��";
            else if (randomValue <= 70f) resourceName = "��ź";
            else if (randomValue <= 85f) resourceName = "ö";
            else if (randomValue <= 95f) resourceName = "��";
            else resourceName = "�ʼ�";
            //0���� �ʱ�ȭ�� �ӽ� ��ųʸ� ���� �̸��� 
            if (!temporaryResources.ContainsKey(resourceName)) {
                temporaryResources[resourceName] = 0;
            }
            //���ҽ� ���� �߰��Ѵ�.
            temporaryResources[resourceName]++;
        }
    }

    private void GetResource()
    {
        //�ӽ��ڿ��� ���ҽ�����
        foreach (var resource in temporaryResources) {
            //Key���� ���� ���� �߰��Ѵ�.
            Resource.Instance.AddResource(resource.Key, resource.Value);
            Debug.Log($"{resource.Value}���� {resource.Key}�� �߰��߽��ϴ�.");
        }
        temporaryResources.Clear(); // �ӽ� �ڿ� ����Ҹ� ���ϴ�
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
