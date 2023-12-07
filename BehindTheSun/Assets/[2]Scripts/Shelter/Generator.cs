using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static Generator Instance;
    public int coalConsumptionRate = 80; // 석탄 소모율 , 추후 보유 아이템에서 따로 석탄 꺼내 쓰는 용도로 개선 필요
    private bool isInteractable = false; // 상호작용 가능한 상태를 나타내는 변수
    private int coalCount = 0; // 발전기에 있는 석탄의 수
    private Coroutine coalConsumptionCoroutine; // 석탄 소비 코루틴
    public Drill drill;
    public YongGwang yongGwang;
    public TV tv;
    private Animator animator;
    private void Start()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        if(animator == null) {
            Debug.Log("오류");
        }

    }

    void Update()
    {

        //겹친상태에서 C키 누르면 작동
        if (Input.GetKeyDown(KeyCode.C) && isInteractable) {
            UseGenerator();
        }
    }

    void UseGenerator()
    {
        EnsureDependenciesInitialized();
        //자원창에서 석탄, 개수만큼 삭제
        if (Resource.Instance.RemoveResource("석탄", (int)coalConsumptionRate)) {
            coalCount += coalConsumptionRate; // 보유 석탄 추가
            Debug.Log("석탄을 추가했습니다. 현재 석탄 수: " + coalCount);

            //석탄이 0초과고 석탄 소비 코루틴이 멈춘상태라면
            if (coalConsumptionCoroutine == null && coalCount > 0) {
                //소비 코루틴 시작
                coalConsumptionCoroutine = StartCoroutine(ConsumeCoal());
                //애니메이션 시작
                animator.SetBool("OnSys", true);
            }
        }
        else {
            Debug.Log("석탄이 부족하여 발전기를 작동시킬 수 없습니다.");
        }
    }

    IEnumerator ConsumeCoal()
    {
        while (true) {
            // 석탄 소비
            coalCount--;
            Debug.Log("석탄을 사용했습니다. 남은 석탄 수: " + coalCount);

            // 석탄이 0이 되면 반복 중지
            if (coalCount <= 0) {
                Debug.Log("석탄이 없어 발전기가 중지되었습니다.");
                StopGenerator();
                break; // 루프 중지
            }

            EnsureDependenciesInitialized();
            ActivateMachines();


            // 12초 대기
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
