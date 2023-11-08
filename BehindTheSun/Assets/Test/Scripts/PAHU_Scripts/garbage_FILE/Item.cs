using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Weapon,
    healingPotion,
    Food,
    DPpotion
}


[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    // 아이템 다수

    public bool Use()
    {
        return false;
    }
}
