using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class YongGwang : MonoBehaviour
{
    private bool isInteractable = false;
    public bool GenGage = false;
    private Animator animator;

    GameObject Young_UI = null;
    public Button GOLDUP;
    public Button GOLDDOWN;
    public Button IRONUP;
    public Button IRONDOWN;
    public Button PUSHButton;
    public Button GETButton;
    public TMP_Text GOLDCOUNT;
    public TMP_Text IRONCOUNT;
    public TMP_Text Coal_TEXT;
    public TMP_Text G_GOLDCOUNT;
    public TMP_Text G_IRONCOUNT;

    private int goldToRefine = 0;
    private int ironToRefine = 0;
    private int refinedIron = 0;
    private int refinedGold = 0;
    int coalCount = 0;
    int coalToUse =0;

    private void Start()
    {
        Young_UI = transform.GetChild(0).gameObject;
        if (Young_UI != null) {
            Young_UI.SetActive(false);
        }
        animator = GetComponent<Animator>();

        GOLDUP.onClick.AddListener(OnGoldUpButtonClicked);
        GOLDDOWN.onClick.AddListener(OnGoldDownButtonClicked);
        IRONUP.onClick.AddListener(OnIronUpButtonClicked);
        IRONDOWN.onClick.AddListener(OnIronDownButtonClicked);
        PUSHButton.onClick.AddListener(OnPushButtonClicked);
        GETButton.onClick.AddListener(OnGetButtonClicked);
    }

    private void Update()
    {
        if (isInteractable && GenGage && Input.GetKeyDown(KeyCode.C)) {
            YoungUIOn();
        }
    }

    void YoungUIOn()
    {
        if (Young_UI != null) {
            Young_UI.SetActive(!Young_UI.activeSelf);
        }
    }

    void OnGoldUpButtonClicked()
    {
        goldToRefine++; // 금을 올리는 동작을 정의합니다.
        UpdateUI(); // UI를 업데이트합니다.
    }

    void OnGoldDownButtonClicked()
    {
        goldToRefine = Mathf.Max(0, goldToRefine - 1); // 금을 내리는 동작을 정의합니다.
        UpdateUI(); // UI를 업데이트합니다.
    }

    void OnIronUpButtonClicked()
    {
        ironToRefine++; // 철을 올리는 동작을 정의합니다.
        UpdateUI(); // UI를 업데이트합니다.
    }

    void OnIronDownButtonClicked()
    {
        ironToRefine = Mathf.Max(0, ironToRefine - 1); // 철을 내리는 동작을 정의합니다.
        UpdateUI(); // UI를 업데이트합니다.
    }

    void OnPushButtonClicked()
    {
        animator.SetBool("OnSys", true);

        Resource.Instance.RemoveResource("석탄", coalToUse);
        Resource.Instance.RemoveResource("철", ironToRefine);
        Resource.Instance.RemoveResource("금", goldToRefine);

        if(ironToRefine > 0 || goldToRefine >0) 
        {
            StartCoroutine(RefineResources(coalToUse, ironToRefine, goldToRefine));
            YoungUIOn();
        }
    }


    private IEnumerator RefineResources(int coalUsed,int ironUsed,int goldUsed)
    {
        yield return new WaitForSeconds(5f); // 제련 시간 5초 대기
        refinedIron += ironUsed;
        refinedGold += goldUsed;

        // ironToRefine과 goldToRefine 값을 초기화
        ironToRefine = 0;
        goldToRefine = 0;
        UpdateUI(); // UI 업데이트
        animator.SetBool("OnSys", false);
    }

    void OnGetButtonClicked()
    {

        // RefineResources 함수의 종료 후 정제된 자원을 추가하는 작업
        Resource.Instance.AddResource("제련된 철",refinedIron);
        Resource.Instance.AddResource("제련된 금", refinedGold);

        //초기화
        refinedGold = 0;
        refinedIron = 0;

        // UI를 업데이트
        UpdateUI();
    }
    void UpdateUI()
    {
        coalCount = (ironToRefine + goldToRefine);
        coalToUse = Mathf.Max(1, ((coalCount-1) / 10)+1);
        if (ironToRefine == 0 && goldToRefine == 0) {
            coalToUse = 0;
        }
        Coal_TEXT.text = coalToUse.ToString();
        GOLDCOUNT.text = goldToRefine.ToString();
        IRONCOUNT.text = ironToRefine.ToString();
        G_GOLDCOUNT.text = refinedGold.ToString();
        G_IRONCOUNT.text = refinedIron.ToString();
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInteractable = false;
        if (Young_UI != null) {
            Young_UI.SetActive(false);
        }
    }
}
