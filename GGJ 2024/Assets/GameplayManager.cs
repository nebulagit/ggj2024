using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    TickeSpot atualTickeSpot; public TickeSpot AtualTickeSpot { get => atualTickeSpot; set => atualTickeSpot = value; }

    int day = 1; public int Day { get => day; }
    int money; public int Money { get => money; }
    CurrentAnimal currentAnimal; public CurrentAnimal CurrentAnimal { get => currentAnimal; }

    int startSatisfationDecrease = 120;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        currentAnimal = AnimalManager.Instance.GenerateAnimal();
        AnimalPrefab animalPrefab = Instantiate(AnimalData.Instance.Animals[currentAnimal.Species].Prefab, Vector2.zero, Quaternion.identity).GetComponent<AnimalPrefab>();
        animalPrefab.TickeSpots[currentAnimal.FavoriteTickleSpot].IsFavorite = true;
        animalPrefab.TickeSpots[currentAnimal.DetestableTickleSpot].IsDetestable = true;

        Hud.Instance.UpdateDay();
        Hud.Instance.UpdateMoney();
        Hud.Instance.UpdateCurrentAnimal();
        Hud.Instance.UpdateAnimalSatisfation();
    }
    void FixedUpdate()
    {
        startSatisfationDecrease = Mathf.Clamp(--startSatisfationDecrease, 0, startSatisfationDecrease);
        if (startSatisfationDecrease == 0)
        {
            if (!atualTickeSpot)
                currentAnimal.Satisfaction -= 0.07f - currentAnimal.Hapyness / 100;
            else
            {
                if (atualTickeSpot.IsFavorite)
                {
                    currentAnimal.Satisfaction += (0.07f - currentAnimal.Anger / 100) * atualTickeSpot.Validity;
                    atualTickeSpot.Validity = Mathf.Clamp(atualTickeSpot.Validity - 0.1f, 0, atualTickeSpot.Validity);
                }
            }

            Hud.Instance.UpdateAnimalSatisfation();
        }
    }
}