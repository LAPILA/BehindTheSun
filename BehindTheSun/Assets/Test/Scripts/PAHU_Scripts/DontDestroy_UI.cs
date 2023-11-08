using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy_UI : MonoBehaviour
{
    public static DontDestroy_UI instance;
    void Start()
    {
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

}
