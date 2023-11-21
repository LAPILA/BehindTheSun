using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearItem : MonoBehaviour
{
    private bool hasOccurred = false; 
    public GameObject Target;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && !hasOccurred) {
            Target.SetActive(true);
            hasOccurred = true; 
        }
    }
}
