using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public Slider HP_Gauge;

    float MAX_HP_Value = 100;
    public float Curren_HP_Value = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HP_Gauge.value = Curren_HP_Value / MAX_HP_Value;
    }
}
