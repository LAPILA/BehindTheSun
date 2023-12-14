using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    //발전기,채굴기,용광로,TV 제작대
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
        //초기 오브젝트 Active는 false로 두고, ItemDB에서 설계도 획득 후 c키 클릭 시 true 반환
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
        if (Resource.Instance.GetResourceQuantity("돌") >= 50 &&
            Resource.Instance.GetResourceQuantity("제련된 철") >= 50 &&
            Resource.Instance.GetResourceQuantity("제련된 금") >= 100) 
            {
            Resource.Instance.RemoveResource("돌", 50);
            Resource.Instance.RemoveResource("제련된 철", 50);
            Resource.Instance.RemoveResource("제련된 금", 100);
            generator.SetActive(true);
            genButton.interactable = false;
        }
        else {
            Debug.Log("자원부족");
        }
    }

    private void DrillBuild()
    {
        if (Resource.Instance.GetResourceQuantity("돌") >= 200 &&
            Resource.Instance.GetResourceQuantity("제련된 철") >= 100 &&
            Resource.Instance.GetResourceQuantity("제련된 금") >= 50) {
            Resource.Instance.RemoveResource("돌", 200);
            Resource.Instance.RemoveResource("제련된 철", 100);
            Resource.Instance.RemoveResource("제련된 금", 50);
            Drill.SetActive(true);
            DrillButton.interactable = false;
        }
        else {
            Debug.Log("자원부족");
        }
    }

    private void YongGwangBuild()
    {
        if (Resource.Instance.GetResourceQuantity("돌") >= 100 &&
            Resource.Instance.GetResourceQuantity("제련된 철") >= 50) {
            Resource.Instance.RemoveResource("돌", 100);
            Resource.Instance.RemoveResource("제련된 철", 50);
            YongGwang.SetActive(true);
            YongGwangButton.interactable = false;
        }
        else {
            Debug.Log("자원부족");
        }
    }

    private void TVBuild()
    {
        if (Resource.Instance.GetResourceQuantity("돌") >= 50 &&
            Resource.Instance.GetResourceQuantity("제련된 철") >= 100 &&
            Resource.Instance.GetResourceQuantity("제련된 금") >= 100) {
            Resource.Instance.RemoveResource("돌", 50);
            Resource.Instance.RemoveResource("제련된 철", 100);
            Resource.Instance.RemoveResource("제련된 금", 100);
            TV.SetActive(true);
            TVButton.interactable = false;
        }
        else {
            Debug.Log("자원부족");
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
