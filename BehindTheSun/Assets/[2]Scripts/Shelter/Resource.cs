using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public static Resource Instance; //싱글톤 - 어디서든 사용가능

    // 기본 자원 목록 - 이름지정용 List
    private List<string> defaultResources = new List<string> {
        "돌", "나무", "철", "제련된 철", "금", "제련된 금", "석탄", "초석"
    };
    //List이름에 따른 수 정리
    private Dictionary<string, int> resources = new Dictionary<string, int>();

    void Awake()
    {   //아이템관련 정보 제거 안되게!
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 기본 자원 초기화 - 추후 제거
            foreach (string resource in defaultResources) {
                resources.Add(resource, 100);
            }
        }
        else {
            Destroy(gameObject);
        }
    }

    public void AddResource(string name, int quantity)
    {
        if (resources.ContainsKey(name)) {
            resources[name] += quantity;
            Debug.Log($"Added {quantity} of {name}. Total: {resources[name]}");
        }
        else {
            Debug.Log($"{name} 존재 안함");
        }
    }


    public bool RemoveResource(string name, int quantity) //아이템 제거 방법 RemoveResource(name,수)
    {
        if (resources.ContainsKey(name) && resources[name] >= quantity) {
            resources[name] -= quantity;
            Debug.Log($"Removed {quantity} of {name}. Remaining: {resources[name]}");
            return true;
        }
        else {
            Debug.Log($"Not enough {name} to remove.");
            return false;
        }
    }
    //아이템 개수 확인 
    public int GetResourceQuantity(string name)
    {
        return resources.ContainsKey(name) ? resources[name] : 0;
    }
}
