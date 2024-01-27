using UnityEngine;

public class AnimalPrefab : MonoBehaviour
{
    [SerializeField] TickleSpot[] tickleSpots; public TickleSpot[] TickleSpots { get => tickleSpots; set => tickleSpots = value; }
    [SerializeField] Sprite normal, calm, bored, desesperated, happy, irrited;
    [SerializeField] SpriteRenderer eyes, mouth, body, leftEar, rightEar, leftFoot, rightFoot, leftFrontFoot, rightFrontFoot, tail;
    [SerializeField] GameObject hearth;
    [SerializeField] Vector3 hearthSpawnPoint;
    [SerializeField] Transform face;

    float hearthSpawn, originalFacePosition;
    int laughtDirection = -1;

    void Start()
    {
        for (int ts = 0; ts < tickleSpots.Length; ts++)
            tickleSpots[ts].Index = ts;

        body.color = GameplayManager.Instance.CurrentAnimal.Color;
        leftEar.color = GameplayManager.Instance.CurrentAnimal.Color;
        rightEar.color = GameplayManager.Instance.CurrentAnimal.Color;
        leftFoot.color = GameplayManager.Instance.CurrentAnimal.Color;
        rightFoot.color = GameplayManager.Instance.CurrentAnimal.Color;
        leftFrontFoot.color = GameplayManager.Instance.CurrentAnimal.Color;
        rightFrontFoot.color = GameplayManager.Instance.CurrentAnimal.Color;
        tail.color = GameplayManager.Instance.CurrentAnimal.Color;

        originalFacePosition = face.position.y;
    }
    void FixedUpdate()
    {
        if (GameplayManager.Instance.TimeSec > 0 && GameplayManager.Instance.Starting == 0)
        {
            if (GameplayManager.Instance.CurrentAnimal.Irritation > 50 && GameplayManager.Instance.CurrentAnimal.Breathing > 25)
            {
                eyes.sprite = irrited;
                mouth.sprite = AnimalData.Instance.Animals[GameplayManager.Instance.CurrentAnimal.Species].IrritatedMouth;
            }
            else
            {
                if (GameplayManager.Instance.CurrentAnimal.Breathing >= 25)
                {
                    if (GameplayManager.Instance.AtualTickleSpot && GameplayManager.Instance.AtualTickleSpot.IsFavorite)
                    {
                        if (GameplayManager.Instance.CurrentAnimal.Enjoying < 50)
                        {
                            eyes.sprite = normal;
                            mouth.sprite = AnimalData.Instance.Animals[GameplayManager.Instance.CurrentAnimal.Species].HappyMouth;
                        }
                        else
                        {
                            eyes.sprite = happy;
                            mouth.sprite = AnimalData.Instance.Animals[GameplayManager.Instance.CurrentAnimal.Species].LaughtMouth;
                        }
                    }
                    else if (GameplayManager.Instance.AtualTickleSpot && GameplayManager.Instance.AtualTickleSpot.IsDetestable)
                    {
                        eyes.sprite = irrited;
                        mouth.sprite = AnimalData.Instance.Animals[GameplayManager.Instance.CurrentAnimal.Species].IrritatedMouth;
                    }
                    else
                    {
                        eyes.sprite = GameplayManager.Instance.CurrentAnimal.Enjoying < 30 ? eyes.sprite = bored : GameplayManager.Instance.CurrentAnimal.Enjoying > 70 ? eyes.sprite = happy : eyes.sprite = normal;
                        mouth.sprite = GameplayManager.Instance.CurrentAnimal.Enjoying < 50 ? mouth.sprite = AnimalData.Instance.Animals[GameplayManager.Instance.CurrentAnimal.Species].BoredMouth : AnimalData.Instance.Animals[GameplayManager.Instance.CurrentAnimal.Species].HappyMouth;
                    }
                }
                else
                {
                    eyes.sprite = desesperated;
                    mouth.sprite = GameplayManager.Instance.AtualTickleSpot && GameplayManager.Instance.AtualTickleSpot.IsFavorite ? AnimalData.Instance.Animals[GameplayManager.Instance.CurrentAnimal.Species].DesesperatedMouth : AnimalData.Instance.Animals[GameplayManager.Instance.CurrentAnimal.Species].BoredMouth;
                }
            }

            if (GameplayManager.Instance.AtualTickleSpot && !GameplayManager.Instance.AtualTickleSpot.IsDetestable && GameplayManager.Instance.CurrentAnimal.Breathing >= 25)
            {
                hearthSpawn += (GameplayManager.Instance.AtualTickleSpot.IsFavorite && GameplayManager.Instance.CurrentAnimal.Enjoying < 50 ? 2 : GameplayManager.Instance.AtualTickleSpot.IsFavorite && GameplayManager.Instance.CurrentAnimal.Enjoying >= 50 ? 4 : 1) * GameplayManager.Instance.AtualTickleSpot.Validity;
                if (hearthSpawn > 22)
                {
                    Instantiate(hearth, hearthSpawnPoint, Quaternion.identity);
                    hearthSpawn -= 22;
                }
            }

            if (GameplayManager.Instance.AtualTickleSpot && GameplayManager.Instance.AtualTickleSpot.IsFavorite)
            {
                float laughIntensity = GameplayManager.Instance.CurrentAnimal.Breathing < 25 ? 8f : GameplayManager.Instance.CurrentAnimal.Enjoying < 50 ? 0.5f : 4f;

                face.transform.position = new Vector2(face.transform.position.x, Mathf.Lerp(face.transform.position.y, face.transform.position.y + 0.025f * laughtDirection * laughIntensity, 0.3f));
                laughtDirection = face.transform.position.y < originalFacePosition - 0.05f ? 1 : face.transform.position.y > originalFacePosition + 0.05f ? -1 : laughtDirection;
            }
        }
    }
}