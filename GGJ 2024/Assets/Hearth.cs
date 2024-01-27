using UnityEngine;

public class Hearth : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    
    int time = 120;

    void FixedUpdate()
    {
        time --;
        if (time < 0)
            spriteRenderer.color = new Vector4(255, 255, 255, spriteRenderer.color.a - 0.05f);
        if (spriteRenderer.color.a < 0)
            Destroy(gameObject);

        transform.position = new Vector2(transform.position.x, transform.position.y + (GameplayManager.Instance.AtualTickleSpot && GameplayManager.Instance.AtualTickleSpot.IsFavorite ? 0.1f : 0.04f));
    }
}