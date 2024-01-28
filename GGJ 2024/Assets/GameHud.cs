using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
    [SerializeField] RectTransform enjoy, breath, totalBar;
    [SerializeField] Image breathImage;
    [SerializeField] GameObject popup;

    void FixedUpdate()
    {
        enjoy.anchoredPosition = new Vector2(0, 950 * (GameplayManager.Instance.CurrentAnimal.Enjoying / 100));
        breath.sizeDelta = new Vector2(GameplayManager.Instance.CurrentAnimal.Breathing * 2, GameplayManager.Instance.CurrentAnimal.Breathing * 2);

        breathImage.color = GameplayManager.Instance.CurrentAnimal.Breathing > 50 ? Color.green : GameplayManager.Instance.CurrentAnimal.Breathing > 25 ? Color.yellow : Color.red;

        if (GameplayManager.Instance.FinishedAnimals.Count > 0)
        {
            float totalBarLength = 0;
            foreach(float animal in GameplayManager.Instance.FinishedAnimals)
                totalBarLength += animal;

            totalBar.sizeDelta = new Vector2(Mathf.Clamp(Mathf.Lerp(totalBar.sizeDelta.x, totalBarLength, 0.1f), 0, 620), totalBar.sizeDelta.y);
        }

        if (popup.activeSelf)
            totalBar.sizeDelta = new Vector2(0, totalBar.sizeDelta.y);
    }
}