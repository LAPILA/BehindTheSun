using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public Slider HP_Gauge;
    public Slider ES_Gauge;
    public Slider BP_Gauge;


    float MAX_HP_Value = 100;
    public float Cr_HP_Value = 10;
    float MAX_ES_Value = 100;
    public float Cr_ES_Value = 10;
    float MAX_BP_Value = 100;
    public float Cr_BP_Value = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HP_Gauge.value = Cr_HP_Value / MAX_HP_Value;
        ES_Gauge.value = Cr_ES_Value / MAX_ES_Value;
        BP_Gauge.value = Cr_BP_Value / MAX_BP_Value;
    }
}
