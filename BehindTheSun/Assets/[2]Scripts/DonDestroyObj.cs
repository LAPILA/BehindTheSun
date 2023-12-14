using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DonDestroyObj : MonoBehaviour
{
    static public DonDestroyObj instance;
    // Start is called before the first frame update
    private void Start()
    {
        if (instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

}
