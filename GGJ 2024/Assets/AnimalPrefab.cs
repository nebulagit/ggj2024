using UnityEngine;

public class AnimalPrefab : MonoBehaviour
{
    [SerializeField] TickeSpot[] tickeSpots; public TickeSpot[] TickeSpots { get => tickeSpots; set => tickeSpots = value; }
}