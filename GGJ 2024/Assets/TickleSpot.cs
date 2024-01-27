using UnityEngine;

public class TickleSpot : MonoBehaviour
{
    [SerializeField] bool isFavorite; public bool IsFavorite { get => isFavorite; set => isFavorite = value; }
    [SerializeField] bool isDetestable; public bool IsDetestable { get => isDetestable; set => isDetestable = value; }

    int index; public int Index { get => index; set => index = value; }

    void OnMouseEnter()
    {
        GameplayManager.Instance.AtualTickleSpot = this;
    }
    void OnMouseExit()
    {
        GameplayManager.Instance.AtualTickleSpot = null;
    }
}