using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    Inventory inven;
    public GameObject inventoryPanel;
    bool activeInventory = false;

    public slot[] slots;
    public Transform slotHolder;

    private void Start()
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<slot>();
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);
    }

    private void SlotChange(int val)
    {
        for(int i = 0; i< slots.Length; i++)
        {
            if(i<  inven.SlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt++;
    }

    void RedrawSlotUI()
    {
        for(int i = 0; i<slots.Length;i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
