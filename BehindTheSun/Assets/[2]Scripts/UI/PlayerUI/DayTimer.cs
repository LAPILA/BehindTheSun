using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DayTimer : MonoBehaviour
{
    public static DayTimer instance;
    [SerializeField] float setTime = 0.0f;
    int day;
    bool IsNight;
    public float dayTime;
    public float nightTime;
    public bool nextCheck;
    
    void Start()
    {
        //낮인 상태로 시작 설정
        IsNight = false;
        setTime = 0.0f;
        day = 0;
        dayTime = 0;
        nightTime = 0;
        nextCheck = false;
    }

    
    void Update()
    {
        setTime += Time.deltaTime;
        
        if(0.0f< setTime && setTime < 480.0f)
        {
            dayTime += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 180 + (dayTime * 0.375f));
        }

        if (setTime >= 480.0f && !IsNight)
        {
            IsNight = true;
            dayTime = 0;
            Debug.Log("밤입니다");
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if(setTime > 480.0f && setTime < 960.0f)
        {
            nightTime += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, nightTime * 0.375f);
        }

        if(setTime >= 960.0f && IsNight)
        {
            nightTime = 0;
            transform.rotation = Quaternion.Euler(0, 0, 180);
            setTime = 0.0f;
            IsNight = false;
            nextCheck = true;
            day++;
            nextCheck = false;
            Debug.Log("낮입니다!");
        }

    }

}
