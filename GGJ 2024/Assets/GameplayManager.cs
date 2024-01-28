using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    TickleSpot atualTickleSpot; public TickleSpot AtualTickleSpot { get => atualTickleSpot; set => atualTickleSpot = value; }

    int day = 1; public int Day { get => day; }
    int money; public int Money { get => money; }
    int timeSec; public int TimeSec { get => timeSec; }
    int starting; public int Starting { get => starting; }
    CurrentAnimal currentAnimal; public CurrentAnimal CurrentAnimal { get => currentAnimal; }
    AnimalPrefab animalPrefab;

    int changingAnimalTickleSpots, timeMil = 0;
    List<float> finishedAnimals = new(); public List<float> FinishedAnimals { get => finishedAnimals; }

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        ResetAnimal();
        Hud.Instance.UpdateDay();
        Hud.Instance.UpdateMoney();
    }
    void FixedUpdate()
    {
        starting = Mathf.Clamp(--starting, 0, starting);
        if (starting == 0 && timeSec > 0)
        {
            if (!atualTickleSpot)
            {
                currentAnimal.Enjoying = ReduceEnjoying(1f);
                currentAnimal.Irritation = ReduceIrritation(0.5f);
            }
            else if (Input.GetMouseButton(0))
            {
                if (atualTickleSpot.IsDetestable)
                {
                    currentAnimal.Irritation = GetsIrritation(1f);
                    currentAnimal.Enjoying = ReduceEnjoying(8f);
                }
                else
                    Tickles(atualTickleSpot.IsFavorite);
            }
            if (atualTickleSpot && atualTickleSpot.IsFavorite)
                currentAnimal.Breathing = Mathf.Clamp(currentAnimal.Enjoying < 50 ? RecoverBreath(0.75f) : currentAnimal.Breathing - 1f + currentAnimal.Breath / 16, 0, 100);
            else
                currentAnimal.Breathing = RecoverBreath(1f);

            Hud.Instance.UpdateAnimalStatus();

            timeMil ++;
            if (timeMil >= 60)
            {
                timeSec --;
                timeMil = 0;
                Hud.Instance.UpdateTime();
            }
        }
        else
        {
            if (timeSec > 0)
                animalPrefab.transform.position = new Vector2(starting > 0 && animalPrefab.transform.position.x > 0 ? animalPrefab.transform.position.x - 0.2f : 0, animalPrefab.transform.position.y);
            else
            {
                timeMil --;
                if (timeMil == -10)
                    finishedAnimals.Add(currentAnimal.Enjoying *1.54f);

                animalPrefab.transform.position = new Vector2(timeMil > -120 ? 0 : animalPrefab.transform.position.x - 0.2f, animalPrefab.transform.position.y);
                if (animalPrefab.transform.position.x <= -20)
                {
                    Destroy(animalPrefab);
                    ResetAnimal();
                }        
            }
        }

        if (atualTickleSpot && (atualTickleSpot.IsFavorite || atualTickleSpot.IsDetestable))
        {}
        else
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
            int whatRarity = (int)Mathf.Floor(Random.Range(5, 10.99f));
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

        currentAnimal.Color = AnimalData.Instance.Animals[_species].AvaiableColors[(int)Mathf.Floor(Random.Range(0, AnimalData.Instance.Animals[_species].AvaiableColors.Length - 1 + 0.99f))];
    }
    void Tickles(bool _isTheFavoriteTickleSpot)
    {
        if (currentAnimal.Breathing >= 25)
        {
            if (currentAnimal.Breathing > 50)
                currentAnimal.Enjoying = ImprovesEnjoying(_isTheFavoriteTickleSpot ? 4f : 1f);
            else
                currentAnimal.Enjoying = ImprovesEnjoying(_isTheFavoriteTickleSpot ? 2f : 0.5f);
        }
        else
        {
            currentAnimal.Irritation = GetsIrritation(2f);
            currentAnimal.Enjoying = ReduceEnjoying(4f);
        }
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
        return Mathf.Clamp(currentAnimal.Enjoying + (0.02f + currentAnimal.Happyness / 150) * _modifier * atualTickleSpot.Validity, 0, 100);
    }
    float GetsIrritation(float _modifier)
    {
        return Mathf.Clamp(currentAnimal.Irritation + (0.4f + currentAnimal.Anger / 40) * _modifier, 0, 100);
    }
    void ResetAnimal()
    {
        currentAnimal = GenerateAnimal();
        animalPrefab = Instantiate(AnimalData.Instance.Animals[currentAnimal.Species].Prefab, new Vector2(20, -0.5f), Quaternion.identity).GetComponent<AnimalPrefab>();
        animalPrefab.transform.localScale = new Vector2(0.9f, 0.9f);
        DefineTickleSpots(currentAnimal, currentAnimal.Species);
        starting = 120;
        timeSec = 20;
        timeMil = 0;
        Hud.Instance.UpdateCurrentAnimal();
        Hud.Instance.UpdateAnimalStatus();
        Hud.Instance.UpdateTime();
    }
}