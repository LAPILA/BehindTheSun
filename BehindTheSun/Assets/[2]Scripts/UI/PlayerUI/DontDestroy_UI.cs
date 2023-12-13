using UnityEngine;

public class DontDestroy_UI : MonoBehaviour
{
    public static DontDestroy_UI instance;

    void Awake()
    {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
}
