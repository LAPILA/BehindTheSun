using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<Item> itemDB = new List<Item>();
    public GameObject fieldItemPrefab;
    public Vector2[] pos;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0, 3)]);
        }
    }

}
