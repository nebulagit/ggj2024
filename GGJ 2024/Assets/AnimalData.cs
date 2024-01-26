using UnityEngine;

public class AnimalData : MonoBehaviour
{
    public static AnimalData Instance;

    [SerializeField] Animal[] animals; public Animal[] Animals { get => animals; }
    [SerializeField] string[] names; public string[] Names { get => names; }

    void Awake()
    {
        Instance = this;
    }
}