using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabGo : MonoBehaviour
{
    private gamemanager gm;
    public GameObject go;
    //public bool laboratoy_open = false;
    void Start()
    {
        gm=FindObjectOfType<gamemanager>();
        go.SetActive(false);
    }

    
    void Update()
    {
        if(gm.laboratoy_open)
        {
            go.SetActive(true);
        }
        else
        {
            go.SetActive(false);
        }
    }
}
