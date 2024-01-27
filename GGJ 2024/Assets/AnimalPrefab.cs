using UnityEngine;

public class AnimalPrefab : MonoBehaviour
{
    [SerializeField] TickleSpot[] tickleSpots; public TickleSpot[] TickleSpots { get => tickleSpots; set => tickleSpots = value; }
    [SerializeField] Sprite normal, calm, bored, desesperated, happy, irrited;
    [SerializeField] SpriteRenderer eyes;
    [SerializeField] GameObject hearth;
    [SerializeField] Vector3 hearthSpawnPoint;

    float hearthSpawn;

    void Start()
    {
        for (int ts = 0; ts < tickleSpots.Length; ts++)
            tickleSpots[ts].Index = ts;
    }
    void FixedUpdate()
    {
        if (GameplayManager.Instance.TimeSec > 0 && GameplayManager.Instance.Starting == 0)
        {
            if (GameplayManager.Instance.CurrentAnimal.Irritation > 50 && GameplayManager.Instance.CurrentAnimal.Breathing > 25)
                eyes.sprite = irrited;
            else
            {
                if (GameplayManager.Instance.CurrentAnimal.Breathing >= 25)
                {
                    if (GameplayManager.Instance.AtualTickleSpot && GameplayManager.Instance.AtualTickleSpot.IsFavorite)
                        eyes.sprite = happy;
                    else if (GameplayManager.Instance.AtualTickleSpot && GameplayManager.Instance.AtualTickleSpot.IsDetestable)
                        eyes.sprite = irrited;
                    else
                        eyes.sprite = GameplayManager.Instance.CurrentAnimal.Enjoying < 30 ? eyes.sprite = bored : GameplayManager.Instance.CurrentAnimal.Enjoying > 70 ? eyes.sprite = happy : eyes.sprite = normal;
                }
                else
                    eyes.sprite = desesperated;
            }

            if (GameplayManager.Instance.AtualTickleSpot && !GameplayManager.Instance.AtualTickleSpot.IsDetestable && GameplayManager.Instance.CurrentAnimal.Breathing >= 25)
            {
                hearthSpawn += (GameplayManager.Instance.AtualTickleSpot.IsFavorite ? 4 : 1) * GameplayManager.Instance.AtualTickleSpot.Validity;
                if (hearthSpawn > 22)
                {
                    Instantiate(hearth, hearthSpawnPoint, Quaternion.identity);
                    hearthSpawn -= 22;
                }
            }
        }
    }
}