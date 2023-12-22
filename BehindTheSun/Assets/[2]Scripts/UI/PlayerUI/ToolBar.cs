using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ToolBar : MonoBehaviour
{
    public static ToolBar Instance { get; private set; }
    public gamemanager gameManager;
    public Player_attack Player_Attack;

    int KP;

    public GameObject highlight_1;
    public GameObject highlight_2;
    public GameObject highlight_3;
    public GameObject highlight_4;
    public GameObject highlight_5;
    public GameObject highlight_6;
    public GameObject highlight_7;
    public GameObject highlight_8;
    public GameObject highlight_9;
    public GameObject highlight_10;

    public Text PickAxe_Value_num;
    public Text Pistol_bullet_num;
    public Text Rifle_bullet_num;
    public Text Shotgun_bullet_num;
    public Text Painkiller_num;
    public Text Antibiotic_num;
    public Text Potato_num;
    public Text Meet_num;
    public Text Canned_Food_num;
    public Text Antidepressants_num;

    bool Rifle_Unlock;
    bool Shotgun_Unlock;

    bool PickAxe_Active;
    bool Pistol_Active;
    bool Rifle_Active;
    bool Shotgun_Active;
    bool Painkiller_Active;
    bool Antibiotic_Active;
    bool Potato_Active;
    bool Meet_Active;
    bool Canned_Food_Active;
    bool Antidepressants_Active;

    float MAX_PickAxe_Value = 100; // 곡괭이 최대 내구도
    public float Cr_PickAxe_Value = 100; // 곡괭이 현재 내구도
    public float Pistol_bullet = 100; // 권총 총알
    public float Rifle_bullet = 100;
    public float Shotgun_bullet = 100;
    public float Painkiller_quantity = 100;
    public float Antibiotic_quantity = 100;
    public float Potato_quantity = 100;
    public float Meet_quantity = 100;
    public float Canned_Food_quantity = 100;
    public float Antidepressants_quantity = 100;


    void Start()
    {
        highlight_1.SetActive(true);
        highlight_2.SetActive(false);
        highlight_3.SetActive(false);
        highlight_4.SetActive(false);
        highlight_5.SetActive(false);
        highlight_6.SetActive(false);
        highlight_7.SetActive(false);
        highlight_8.SetActive(false);
        highlight_9.SetActive(false);
        highlight_10.SetActive(false);
        PickAxe_Active = true;
        Pistol_Active = false;
        Rifle_Active = false;
        Shotgun_Active = false;
        Painkiller_Active = false;
        Antibiotic_Active = false;
        Potato_Active = false;
        Meet_Active = false;
        Canned_Food_Active = false;
        Antidepressants_Active = false;
        Rifle_Unlock = false;
        Shotgun_Unlock = false;
    }

    void Update()
    {
        KP = gameManager.Return_KP();

        if (!Rifle_Unlock && KP >= 20)
        {
            Rifle_Unlock = true;
        }

        if(!Shotgun_Unlock && KP >= 40)
        {
            Shotgun_Unlock = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Highright_All_off();
            highlight_1.SetActive(true);
            PickAxe_Active = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Highright_All_off();
            highlight_2.SetActive(true);
            Pistol_Active = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Highright_All_off();
            highlight_3.SetActive(true);
            Rifle_Active = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Highright_All_off();
            highlight_4.SetActive(true);
            Shotgun_Active = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Highright_All_off();
            highlight_5.SetActive(true);
            Painkiller_Active = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Highright_All_off();
            highlight_6.SetActive(true);
            Antibiotic_Active = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Highright_All_off();
            highlight_7.SetActive(true);
            Potato_Active = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Highright_All_off();
            highlight_8.SetActive(true);
            Meet_Active = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Highright_All_off();
            highlight_9.SetActive(true);
            Canned_Food_Active = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Highright_All_off();
            highlight_10.SetActive(true);
            Antidepressants_Active = true;
        }

        //곡괭이 사용
        if (Input.GetKeyDown(KeyCode.Z) && PickAxe_Active)
        {
            if(Cr_PickAxe_Value > 0)
            {
                Debug.Log("곡괭이 사용");
                Cr_PickAxe_Value--;
            }
            else
            {
                Debug.Log("내구도 부족");
            }
        }

        //피스톨 사용
        if (Input.GetKeyDown(KeyCode.Z) && Pistol_Active && !gameManager.pistol_use)
        {
            if(Pistol_bullet > 0)
            {
                Debug.Log("피스톨 사용");
                Player_Attack.Shoot();
                gameManager.Use_Pistol();
                Pistol_bullet--;
            }
            else
            {
                Debug.Log("총알 부족");
            }
            Invoke("Reload_gun", 1);
        }

        //라이플 사용
        if (Rifle_Unlock &&Input.GetKeyDown(KeyCode.Z) && Rifle_Active && !gameManager.rifle_use)
        {
            if (Rifle_bullet > 0)
            {
                Debug.Log("라이플 사용");
                Player_Attack.Shoot();
                gameManager.Use_Riflegun();
                Rifle_bullet--;
            }
            else
            {
                Debug.Log("총알 부족");
            }
            Invoke("Reload_gun", 1);
        }

        //샷건 사용
        if (Shotgun_Unlock && Input.GetKeyDown(KeyCode.Z) && Shotgun_Active && !gameManager.shotgun_use)
        {
            if (Shotgun_bullet > 0)
            {
                Debug.Log("샷건 사용");
                Player_Attack.Shoot();
                gameManager.Use_Shotgun();
                Shotgun_bullet--;
            }
            else
            {
                Debug.Log("총알 부족");
            }
            Invoke("Reload_gun", 1);
        }

        //진통제 사용
        if (Input.GetKeyDown(KeyCode.Z) && Painkiller_Active)
        {
            if(Painkiller_quantity > 0)
            {
                Debug.Log("진통제 사용");
                Painkiller_quantity--;
                gameManager.Use_PainKiller();
            }
            else
            {
                Debug.Log("진통제가 없습니다");
            }
        }

        //항생제 사용
        if (Input.GetKeyDown(KeyCode.Z) && Antibiotic_Active)
        {
            if (Antibiotic_quantity > 0)
            {
                Debug.Log("항생제 사용");
                Antibiotic_quantity--;
                gameManager.Use_Antibiotic();
            }
            else
            {
                Debug.Log("항생제가 없습니다");
            }
        }

        //감자 먹기
        if (Input.GetKeyDown(KeyCode.Z) && Potato_Active)
        {
            if (Potato_quantity > 0)
            {
                Debug.Log("감자 먹기");
                Potato_quantity--;
                gameManager.Use_Potato();
            }
            else
            {
                Debug.Log("감자가 없습니다");
            }
            
        }

        //고기 먹기
        if (Input.GetKeyDown(KeyCode.Z) && Meet_Active)
        {
            if (Meet_quantity > 0)
            {
                Debug.Log("고기 먹기");
                Meet_quantity--;
                gameManager.Use_Meet();
            }
            else
            {
                Debug.Log("고기가 없습니다");
            }
        }

        //통조림 먹기
        if (Input.GetKeyDown(KeyCode.Z) && Canned_Food_Active)
        {
            if (Canned_Food_quantity > 0)
            {
                Debug.Log("통조림 먹기");
                Canned_Food_quantity--;
                gameManager.Use_Canned_Food();
            }
            else
            {
                Debug.Log("통조림이 없습니다");
            }
        }

        //항우울제 먹기
        if(Input.GetKeyDown(KeyCode.Z) && Antidepressants_Active)
        {
            if (Antidepressants_quantity > 0)
            {
                Debug.Log("항우울제 먹기");
                Antidepressants_quantity--;
                gameManager.Use_Antidepressants();
            }
            else
            {
                Debug.Log("항우울제가 없습니다");
            }
        }
    }

    void LateUpdate()
    {
        PickAxe_Value_num.text = Cr_PickAxe_Value.ToString();
        Pistol_bullet_num.text = Pistol_bullet.ToString();
        Rifle_bullet_num.text = Rifle_bullet.ToString();
        Shotgun_bullet_num.text = Shotgun_bullet.ToString();
        Painkiller_num.text = Painkiller_quantity.ToString();
        Antibiotic_num.text = Antibiotic_quantity.ToString();
        Potato_num.text = Potato_quantity.ToString();
        Meet_num.text = Meet_quantity.ToString();
        Canned_Food_num.text = Canned_Food_quantity.ToString();
        Antidepressants_num.text = Antidepressants_quantity.ToString();
}

    void Highright_All_off()
    {
        highlight_1.SetActive(false);
        highlight_2.SetActive(false);
        highlight_3.SetActive(false);
        highlight_4.SetActive(false);
        highlight_5.SetActive(false);
        highlight_6.SetActive(false);
        highlight_7.SetActive(false);
        highlight_8.SetActive(false);
        highlight_9.SetActive(false);
        highlight_10.SetActive(false);
        PickAxe_Active = false;
        Pistol_Active = false;
        Rifle_Active = false;
        Shotgun_Active = false;
        Painkiller_Active = false;
        Antibiotic_Active = false;
        Potato_Active = false;
        Meet_Active = false;
        Canned_Food_Active = false;
        Antidepressants_Active = false;
    }

    public void Reload_gun()
    {
        gameManager.pistol_use = false;
        gameManager.shotgun_use = false;
        gameManager.rifle_use = false;
    }

    public bool Get_PickAxe_Active()
    {
        return PickAxe_Active;
    }
}
