using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance;
    public GameObject Matter_Inventory;

    bool MI_Active;

    public Slider HP_Gauge; // hp ¹Ù
    public Slider ES_Gauge; // es ¹Ù
    public Slider BP_Gauge; // bp ¹Ù

    public Text Wood_Num;
    public Text Stone_Num;
    public Text Iron_Num;
    public Text Gold_Num;
    public Text CornerStone_Num;


    public float MAX_HP_Value = 100;
    public float Cr_HP_Value = 10;
    public float MAX_ES_Value = 200;
    public float Cr_ES_Value = 10;
    public float MAX_BP_Value = 100;
    public float Cr_BP_Value = 10;

    public int Wood_quantity = 100;
    public int Stone_quantity = 100;
    public int Iron_quantity = 100;
    public int Gold_quantity = 100;
    public int CornerStone_quantity = 100;

    void Start()
    {
        MI_Active = false;
        Matter_Inventory.SetActive(MI_Active);

        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            MI_Active = !MI_Active;
            Matter_Inventory.SetActive(MI_Active);
        }
    }

    void LateUpdate()
    {
        HP_Gauge.value = Cr_HP_Value / MAX_HP_Value;
        ES_Gauge.value = Cr_ES_Value / MAX_ES_Value;
        BP_Gauge.value = Cr_BP_Value / MAX_BP_Value;

        Wood_Num.text = Wood_quantity.ToString();
        Stone_Num.text = Stone_quantity.ToString();
        Iron_Num.text = Iron_quantity.ToString();
        Gold_Num.text = Gold_quantity.ToString();
        CornerStone_Num.text= CornerStone_quantity.ToString();
    }

    public void Use_PainKiller()
    {
        Cr_HP_Value += 15;
        if (Cr_HP_Value > MAX_HP_Value)
        {
            Cr_HP_Value = MAX_HP_Value;
        }
    }

    public void Use_Antibiotic()
    {
        Cr_HP_Value += 30;
        if (Cr_HP_Value > MAX_HP_Value)
        {
            Cr_HP_Value = MAX_HP_Value;
        }
    }

    public void Use_Potato()
    {
        Cr_ES_Value += 10;
        if (Cr_ES_Value > MAX_ES_Value)
        {
            Cr_ES_Value = MAX_ES_Value;
        }
    }

    public void Use_Meet()
    {
        Cr_ES_Value += 30;
        if (Cr_ES_Value > MAX_ES_Value)
        {
            Cr_ES_Value = MAX_ES_Value;
        }
    }

    public void Use_Canned_Food()
    {
        Cr_ES_Value += 20;
        if (Cr_ES_Value > MAX_ES_Value)
        {
            Cr_ES_Value = MAX_ES_Value;
        }
    }

    public void Use_Antidepressants()
    {
        Cr_BP_Value += 20;
        if(Cr_BP_Value > MAX_BP_Value)
        {
            Cr_BP_Value = MAX_BP_Value;
        }
    }
}
