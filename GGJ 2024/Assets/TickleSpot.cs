using UnityEngine;

public class TickleSpot : MonoBehaviour
{
    [SerializeField] bool isFavorite; public bool IsFavorite { get => isFavorite; set => isFavorite = value; }
    [SerializeField] bool isDetestable; public bool IsDetestable { get => isDetestable; set => isDetestable = value; }

    int index; public int Index { get => index; set => index = value; }
    float validity = 1; public float Validity { get => validity; set => validity = value; }

    void FixedUpdate()
    {
        validity = GameplayManager.Instance.AtualTickleSpot == this ? Mathf.Clamp(validity - 0.0025f, 0, 1) : Mathf.Clamp(validity + 0.01f, 0, 1);
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
            GameplayManager.Instance.AtualTickleSpot = this;
        else
            GameplayManager.Instance.AtualTickleSpot = null;
    }
    void OnMouseExit()
    {
        GameplayManager.Instance.AtualTickleSpot = null;
    }
}