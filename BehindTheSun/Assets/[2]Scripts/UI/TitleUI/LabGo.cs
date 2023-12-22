using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabGo : MonoBehaviour
{
    public gamemanager gm;
    public GameObject go;

    public bool set;


    void Start()
    {
        gm=FindObjectOfType<gamemanager>();
    }

    
    void Update()
    {
        gm = FindObjectOfType<gamemanager>();
        set = gm.get_laboratory_open();
        go.SetActive(set);
    }
}
