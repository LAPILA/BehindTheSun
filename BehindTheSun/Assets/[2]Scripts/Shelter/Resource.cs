using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public static Resource Instance; //�̱��� - ��𼭵� ��밡��

    // �⺻ �ڿ� ��� - �̸������� List
    private List<string> defaultResources = new List<string> {
        "��", "����", "ö", "���õ� ö", "��", "���õ� ��", "��ź", "�ʼ�"
    };
    //List�̸��� ���� �� ����
    private Dictionary<string, int> resources = new Dictionary<string, int>();

    void Awake()
    {   //�����۰��� ���� ���� �ȵǰ�!
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // �⺻ �ڿ� �ʱ�ȭ - ���� ����
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
            Debug.Log($"{name} ���� ����");
        }
    }


    public bool RemoveResource(string name, int quantity) //������ ���� ��� RemoveResource(name,��)
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
    //������ ���� Ȯ�� 
    public int GetResourceQuantity(string name)
    {
        return resources.ContainsKey(name) ? resources[name] : 0;
    }
}
