using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    private bool isInteractable = false;//나중에 상호작용 키(아이템 획득 추가필요)
    public bool GenGage = false;//전기
    private Coroutine resourceGenerationCoroutine;//자원생성 코루틴
    private Dictionary<string, int> temporaryResources = new Dictionary<string, int>();//드릴 자원, 하나더 생성 = 드릴 자원 ->c키누를 시 ->기본 자원으로 이동
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
        //전기가 있고, 자원 생성 코루틴이 중단되면 시작.
        if (GenGage && resourceGenerationCoroutine == null) {
            resourceGenerationCoroutine = StartCoroutine(GenerateResources());

        }
        if (isInteractable && Input.GetKeyDown(KeyCode.C)) {
            GetResource();
        }
    }

  //하루 전체인 16분마다(1분당20개씩) 자원 생성
    private IEnumerator GenerateResources()
    {
        for (int i = 0; i < 16; i++) { // 16분 동안 반복
            yield return new WaitForSeconds(60f); // 1분 대기
            GenerateRandomResources(20); // 1분마다 20개 자원 생성
        }
        resourceGenerationCoroutine = null;
    }

    //자원 랜덤생성 관련 함수
    private void GenerateRandomResources(int count)
    {
        for (int i = 0; i < count; i++) {
            float randomValue = Random.Range(0f, 100f);
            string resourceName = "";
            //랜덤값에 따라 이름을 지정하고,
            if (randomValue <= 40f) resourceName = "돌";
            else if (randomValue <= 70f) resourceName = "석탄";
            else if (randomValue <= 85f) resourceName = "철";
            else if (randomValue <= 95f) resourceName = "금";
            else resourceName = "초석";
            //0으로 초기화한 임시 딕셔너리 값의 이름에 
            if (!temporaryResources.ContainsKey(resourceName)) {
                temporaryResources[resourceName] = 0;
            }
            //리소스 수를 추가한다.
            temporaryResources[resourceName]++;
        }
    }

    private void GetResource()
    {
        //임시자원의 리소스마다
        foreach (var resource in temporaryResources) {
            //Key값과 수를 각각 추가한다.
            Resource.Instance.AddResource(resource.Key, resource.Value);
            Debug.Log($"{resource.Value}개의 {resource.Key}를 추가했습니다.");
        }
        temporaryResources.Clear(); // 임시 자원 저장소를 비웁니다
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
