using UnityEngine;

public class TickeSpot : MonoBehaviour
{
    [SerializeField] bool isFavorite; public bool IsFavorite { get => isFavorite; set => isFavorite = value; }
    [SerializeField] bool isDetestable; public bool IsDetestable { get => isDetestable; set => isDetestable = value; }

    float validity = 100; public float Validity { get => validity; set => validity = value; }

    void OnMouseEnter()
    {
        GameplayManager.Instance.AtualTickeSpot = this;
    }
    void OnMouseExit()
    {
        GameplayManager.Instance.AtualTickeSpot = null;
    }
}