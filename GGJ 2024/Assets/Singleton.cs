using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance;

    void Awake()
    {
        if (Instance && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
            
        DontDestroyOnLoad(gameObject);
    }
}