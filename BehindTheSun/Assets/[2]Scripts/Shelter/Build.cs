using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    //������,ä����,�뱤��,TV ���۴�
    private bool isInteractable = false;
    GameObject Build_UI = null;
    [SerializeField] GameObject generator;
    [SerializeField] GameObject Drill;
    [SerializeField] GameObject YongGwang;
    [SerializeField] GameObject TV;

    public Button genButton;
    public Button YongGwangButton;
    public Button DrillButton;
    public Button TVButton;

    private void Start()
    {
        generator.SetActive(false);
        Drill.SetActive(false);
        YongGwang.SetActive(false);
        TV.SetActive(false);
        //�ʱ� ������Ʈ Active�� false�� �ΰ�, ItemDB���� ���赵 ȹ�� �� cŰ Ŭ�� �� true ��ȯ
        //Build_obj.SetActive(false);
        Build_UI = transform.GetChild(0).gameObject;
        if (Build_UI != null) {
            Build_UI.SetActive(false);
        }
        genButton.onClick.AddListener(GenBuild);
        YongGwangButton.onClick.AddListener(YongGwangBuild);
        DrillButton.onClick.AddListener(DrillBuild);
        TVButton.onClick.AddListener(TVBuild);

    }

    private void Update()
    {
        if(isInteractable && Input.GetKeyDown(KeyCode.C)) {
            BuildUIOn();
        }
    }

    private void BuildUIOn()
    {
        if(Build_UI!=null) {
            Build_UI.SetActive(!Build_UI.activeSelf);
        }


    }

    private void GenBuild()
    {
        if (Resource.Instance.GetResourceQuantity("��") >= 50 &&
            Resource.Instance.GetResourceQuantity("���õ� ö") >= 50 &&
            Resource.Instance.GetResourceQuantity("���õ� ��") >= 100) 
            {
            Resource.Instance.RemoveResource("��", 50);
            Resource.Instance.RemoveResource("���õ� ö", 50);
            Resource.Instance.RemoveResource("���õ� ��", 100);
            generator.SetActive(true);
            genButton.interactable = false;
        }
        else {
            Debug.Log("�ڿ�����");
        }
    }

    private void DrillBuild()
    {
        if (Resource.Instance.GetResourceQuantity("��") >= 200 &&
            Resource.Instance.GetResourceQuantity("���õ� ö") >= 100 &&
            Resource.Instance.GetResourceQuantity("���õ� ��") >= 50) {
            Resource.Instance.RemoveResource("��", 200);
            Resource.Instance.RemoveResource("���õ� ö", 100);
            Resource.Instance.RemoveResource("���õ� ��", 50);
            Drill.SetActive(true);
            DrillButton.interactable = false;
        }
        else {
            Debug.Log("�ڿ�����");
        }
    }

    private void YongGwangBuild()
    {
        if (Resource.Instance.GetResourceQuantity("��") >= 100 &&
            Resource.Instance.GetResourceQuantity("���õ� ö") >= 50) {
            Resource.Instance.RemoveResource("��", 100);
            Resource.Instance.RemoveResource("���õ� ö", 50);
            YongGwang.SetActive(true);
            YongGwangButton.interactable = false;
        }
        else {
            Debug.Log("�ڿ�����");
        }
    }

    private void TVBuild()
    {
        if (Resource.Instance.GetResourceQuantity("��") >= 50 &&
            Resource.Instance.GetResourceQuantity("���õ� ö") >= 100 &&
            Resource.Instance.GetResourceQuantity("���õ� ��") >= 100) {
            Resource.Instance.RemoveResource("��", 50);
            Resource.Instance.RemoveResource("���õ� ö", 100);
            Resource.Instance.RemoveResource("���õ� ��", 100);
            TV.SetActive(true);
            TVButton.interactable = false;
        }
        else {
            Debug.Log("�ڿ�����");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInteractable = false;
        if(Build_UI !=null) {
            Build_UI.SetActive(false );
        }
    }
}
