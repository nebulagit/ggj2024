using UnityEngine;

public class AnimalPrefab : MonoBehaviour
{
    [SerializeField] TickleSpot[] tickleSpots; public TickleSpot[] TickleSpots { get => tickleSpots; set => tickleSpots = value; }

    void Start()
    {
        for (int ts = 0; ts < tickleSpots.Length; ts++)
            tickleSpots[ts].Index = ts;
    }
}