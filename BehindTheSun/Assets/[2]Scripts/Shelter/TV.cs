using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public DayTimer daytimer;
    public gamemanager gamemanager;
    private bool isInteractable = false;
    public bool GenGage = false;
    private Animator animator;
    public int canuse = 0;
    private void Start()
    {
        canuse++;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (daytimer.nextCheck) {
            if (canuse > 1) {
                canuse = 1;
            }
            else {
                canuse++;
            }
        }
        if (GenGage) {
            animator.SetBool("OnSys", true);
        }
        else {
            animator.SetBool("OnSys", false);
        }
        if((canuse>=1)&&GenGage && isInteractable&& Input.GetKeyDown(KeyCode.C)) {
            //¾É¾Æ¼­ TVº¸´Â PlayerAnimation
            gamemanager.Cr_ES_Value += 20;
            if (gamemanager.Cr_ES_Value > gamemanager.MAX_ES_Value) {
                gamemanager.Cr_ES_Value = gamemanager.MAX_ES_Value;
            }
            canuse--;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInteractable = false;
    }
}
