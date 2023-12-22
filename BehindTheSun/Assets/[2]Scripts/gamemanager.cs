using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance;
    public CharacterController2D player;
    public DayTimer daytimer;
    public GameObject Matter_Inventory;
    public Resource Matter_resource;
    public GameObject Crafting_Box;
    public ToolBar TB;

    bool MI_Active;
    bool CB_Active;

    int kill_point = 0;

    public bool pistol_use = false;
    public bool shotgun_use = false;
    public bool rifle_use = false;

    public Slider HP_Gauge; // hp 바
    public Slider ES_Gauge; // es 바
    public Slider BP_Gauge; // bp 바

    float ES_Dcrease_Time;
    float BP_Dcrease_Time;
    float poison_time;

    public Text Wood_Num;
    public Text Stone_Num;
    public Text Iron_Num;
    public Text Gold_Num;
    public Text CornerStone_Num;
    public Text Coral_Num;
    public Text Rough_Iron_Num;
    public Text Rough_Gold_Num;

    public float MAX_HP_Value = 100;
    public float Cr_HP_Value = 100;
    public float MAX_ES_Value = 200;
    public float Cr_ES_Value = 100;
    public float MAX_BP_Value = 100;
    public float Cr_BP_Value = 100;

    public int Wood_quantity = 100;
    public int Stone_quantity = 100;
    public int Iron_quantity = 100;
    public int Gold_quantity = 100;
    public int CornerStone_quantity = 100;
    public int Coral_quantity = 100;
    public int Rough_Iron_quantity = 100;
    public int Rough_Gold_quantity = 100;

    public bool laboratoy_open = false;
    public bool hidden_ending_open = false;
    public bool fightEnd = false;


    void Start()
    {
        MI_Active = false;
        CB_Active = false;
        Matter_Inventory.SetActive(MI_Active);
        Crafting_Box.SetActive(CB_Active);

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

        if (Input.GetKeyDown(KeyCode.B))
        {
            CB_Active = !CB_Active;
            Crafting_Box.SetActive(CB_Active);
        }

        BP_Dcrease_Time += Time.deltaTime;
        ES_Dcrease_Time += Time.deltaTime;
        poison_time += Time.deltaTime;

        if(BP_Dcrease_Time > 60.0f)
        {
            Dcrease_BP();
            BP_Dcrease_Time = 0;
        }

        if(ES_Dcrease_Time > 60.0f)
        {
            Dcrease_ES();
            ES_Dcrease_Time = 0;
        }

        if (!daytimer.IsNight && player.isOut && poison_time >= 1)
        {
            Cr_HP_Value -= 3;
            poison_time = 0;
        }

    }

    void LateUpdate()
    {
        HP_Gauge.value = Cr_HP_Value / MAX_HP_Value;
        ES_Gauge.value = Cr_ES_Value / MAX_ES_Value;
        BP_Gauge.value = Cr_BP_Value / MAX_BP_Value;

        Wood_quantity = Matter_resource.GetResourceQuantity("나무");
        Stone_quantity = Matter_resource.GetResourceQuantity("돌");
        Iron_quantity = Matter_resource.GetResourceQuantity("제련된 철");
        Gold_quantity = Matter_resource.GetResourceQuantity("제련된 금");
        CornerStone_quantity = Matter_resource.GetResourceQuantity("초석");
        Coral_quantity = Matter_resource.GetResourceQuantity("석탄");
        Rough_Gold_quantity = Matter_resource.GetResourceQuantity("금");
        Rough_Iron_quantity = Matter_resource.GetResourceQuantity("철");

        Wood_Num.text = Wood_quantity.ToString();
        Stone_Num.text = Stone_quantity.ToString();
        Iron_Num.text = Iron_quantity.ToString();
        Gold_Num.text = Gold_quantity.ToString();
        CornerStone_Num.text= CornerStone_quantity.ToString();
        Coral_Num.text = Coral_quantity.ToString();
        Rough_Iron_Num.text = Rough_Iron_quantity.ToString();
        Rough_Gold_Num.text = Rough_Gold_quantity.ToString();
    }

    void Dcrease_BP()
    {
        Cr_BP_Value -= 2;
    }

    void Dcrease_ES()
    {
        Cr_ES_Value -= 2;
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

    public void Normal_Bullet_Damaged()
    {
        Cr_HP_Value -= 10;
        //0이하 되면 게임오버
    }

    public void Normal_Wolf_Damaged()
    {
        Cr_HP_Value -= 10;
    }


    public void Boss_Damaged()
    {
        Cr_HP_Value -= 20;
    }

    public void Use_Pistol()
    {
        pistol_use = true;
    }

    public void Use_Shotgun()
    {
        shotgun_use = true;
    }

    public void Use_Riflegun()
    {
        rifle_use = true;
    }

    public void Plus_KillPoint()
    {
        kill_point++;
    }

    public int Return_KP()
    {
        return kill_point;
    }

    public void Repair_PickAxe()
    {
        if(Stone_quantity >= 10)
        {
            if(Iron_quantity >= 2)
            {
                TB.Cr_PickAxe_Value = 100;
                Resource.Instance.RemoveResource("돌", 10);
                Resource.Instance.RemoveResource("제련된 철", 2);
            }
        }
    }

    public void Make_P_bullets()
    {
        
        if (Iron_quantity >= 3)
        {
            if (Coral_quantity >= 1)
            {
                TB.Pistol_bullet += 10;
                Resource.Instance.RemoveResource("제련된 철", 3);
                Resource.Instance.RemoveResource("석탄", 1);
            }
        }
    }

    public void Make_A_bullets()
    {
        
        if (Iron_quantity >= 4)
        {
            if (Coral_quantity >= 2)
            {
                TB.Shotgun_bullet += 10;
                Resource.Instance.RemoveResource("제련된 철", 4);
                Resource.Instance.RemoveResource("석탄", 2);
            }
        }
    }

    public void Make_G_bullets()
    {
        if (Iron_quantity >= 5)
        {
            if (Coral_quantity >= 3)
            {
                TB.Rifle_bullet += 10;
                Resource.Instance.RemoveResource("제련된 철", 5);
                Resource.Instance.RemoveResource("석탄", 3);
            }
        }
    }

    public void Make_Painkiler()
    {

        if (Gold_quantity >= 1)
        {
            TB.Painkiller_quantity += 5;
            Resource.Instance.RemoveResource("제련된 금", 1);
        }
    }

    public void Make_Antibio()
    {
        if (Gold_quantity >= 2)
        {
            TB.Antibiotic_quantity += 3;
            Resource.Instance.RemoveResource("제련된 금", 2);
        }
    }


    public bool get_laboratory_open()
    {
        return laboratoy_open;
    }
}
