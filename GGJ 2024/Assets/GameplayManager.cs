using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    TickleSpot atualTickleSpot; public TickleSpot AtualTickleSpot { get => atualTickleSpot; set => atualTickleSpot = value; }

    int day = 1; public int Day { get => day; }
    int money; public int Money { get => money; }
    CurrentAnimal currentAnimal; public CurrentAnimal CurrentAnimal { get => currentAnimal; }
    AnimalPrefab animalPrefab;

    int starting = 120, changingAnimalTickleSpots;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        currentAnimal = GenerateAnimal();
        animalPrefab = Instantiate(AnimalData.Instance.Animals[currentAnimal.Species].Prefab, Vector2.zero, Quaternion.identity).GetComponent<AnimalPrefab>();
        DefineTickleSpots(currentAnimal, currentAnimal.Species);

        Hud.Instance.UpdateDay();
        Hud.Instance.UpdateMoney();
        Hud.Instance.UpdateCurrentAnimal();
        Hud.Instance.UpdateAnimalStatus();
    }
    void FixedUpdate()
    {
        starting = Mathf.Clamp(--starting, 0, starting);
        if (starting == 0)
        {
            if (!atualTickleSpot)
            {
                currentAnimal.Enjoying = ReduceEnjoying(1f);
                currentAnimal.Irritation = ReduceIrritation(0.5f);
                currentAnimal.Breathing = RecoverBreath(1f);
            }
            else
            {
                if (atualTickleSpot.IsDetestable)
                {
                    currentAnimal.Irritation = GetsIrritation(1f);
                    currentAnimal.Enjoying = ReduceEnjoying(8f);
                }
                else
                    Tickles(atualTickleSpot.IsFavorite);
            }
            Hud.Instance.UpdateAnimalStatus();
        }

        changingAnimalTickleSpots++;
        if (changingAnimalTickleSpots > 180)
        {
            DefineTickleSpots(currentAnimal, currentAnimal.Species);
            changingAnimalTickleSpots = 0;
        }
    }

    CurrentAnimal GenerateAnimal()
    {
        CurrentAnimal newCurrentAnimal = new();

        int whatSpecies = 0;
        bool rarityCheck = false;
        while (!rarityCheck)
        {
            whatSpecies = (int)Mathf.Floor(Random.Range(1, AnimalData.Instance.Animals.Length - 1 + 0.99f));
            int whatRarity = (int)Mathf.Floor(Random.Range(1, 5.99f));
            if (whatRarity >= AnimalData.Instance.Animals[whatSpecies].Rarity)
                rarityCheck = true;
        }
        newCurrentAnimal.Species = whatSpecies;

        int whatName = (int)Mathf.Floor(Random.Range(0, AnimalData.Instance.Names.Length - 1 + 0.99f));
        newCurrentAnimal.Name = AnimalData.Instance.Names[whatName] + " the " + AnimalData.Instance.Animals[newCurrentAnimal.Species].Species.ToLower();

        newCurrentAnimal.Happyness = AnimalData.Instance.Animals[whatSpecies].BaseHappyness;
        newCurrentAnimal.Breath = AnimalData.Instance.Animals[whatSpecies].BaseBreath;
        newCurrentAnimal.Anger = AnimalData.Instance.Animals[whatSpecies].BaseAnger;
        newCurrentAnimal.Enjoying = 30 + Mathf.Clamp(newCurrentAnimal.Happyness * 10 - newCurrentAnimal.Anger * 10, 0, 100);
        return newCurrentAnimal;
    }
    void DefineTickleSpots(CurrentAnimal _currentAnimal, int _species)
    {
        foreach (TickleSpot tickleSpot in animalPrefab.TickleSpots)
        {
            tickleSpot.IsFavorite = false;
            tickleSpot.IsDetestable = false;
        }

        int favoriteTickleSpot = (int)Mathf.Floor(Random.Range(2, AnimalData.Instance.Animals[_species].TickleSpots + 0.99f) - 2);
        _currentAnimal.FavoriteTickleSpot = favoriteTickleSpot;
        int detestableTickleSpot = -1;
        while (detestableTickleSpot == -1)
        {
            detestableTickleSpot = (int)Mathf.Floor(Random.Range(2, AnimalData.Instance.Animals[_species].TickleSpots + 0.99f) - 2);
            if ((detestableTickleSpot == favoriteTickleSpot) || (atualTickleSpot && detestableTickleSpot == atualTickleSpot.Index))
                detestableTickleSpot = -1;
        }
        _currentAnimal.DetestableTickleSpot = detestableTickleSpot;

        animalPrefab.TickleSpots[currentAnimal.FavoriteTickleSpot].IsFavorite = true;
        animalPrefab.TickleSpots[currentAnimal.DetestableTickleSpot].IsDetestable = true;
    }
    void Tickles(bool _isTheFavoriteTickleSpot)
    {
        if (currentAnimal.Breathing >= 25)
            currentAnimal.Enjoying = ImprovesEnjoying(_isTheFavoriteTickleSpot ? 4f : 1f);
        else
        {
            currentAnimal.Irritation = GetsIrritation(2f);
            currentAnimal.Enjoying = ReduceEnjoying(4f);
        }

        currentAnimal.Breathing = _isTheFavoriteTickleSpot ? Mathf.Clamp(currentAnimal.Breathing - 0.7f + currentAnimal.Breath / 16, 0, 100) : RecoverBreath(0.5f);
        currentAnimal.Irritation = ReduceIrritation(1f);
    }
    float RecoverBreath(float _modifier)
    {
        return Mathf.Clamp(currentAnimal.Breathing + (0.03f + currentAnimal.Breath / 50) * _modifier, 0, 100);
    }
    float ReduceIrritation(float _modifier)
    {
        return Mathf.Clamp(currentAnimal.Irritation - (0.2f + currentAnimal.Anger / 40) * _modifier, 0, 100);
    }
    float ReduceEnjoying(float _modifier)
    {
        return Mathf.Clamp(currentAnimal.Enjoying - (0.06f - currentAnimal.Happyness / 150) * _modifier, 0, 100);
    }
    float ImprovesEnjoying(float _modifier)
    {
        return Mathf.Clamp(currentAnimal.Enjoying + (0.02f + currentAnimal.Happyness / 150) * _modifier, 0, 100);
    }
    float GetsIrritation(float _modifier)
    {
        return Mathf.Clamp(currentAnimal.Irritation + (0.4f + currentAnimal.Anger / 40) * _modifier, 0, 100);
    }
}