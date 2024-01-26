using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager Instance;

    public CurrentAnimal GenerateAnimal()
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

        int favoriteTickleSpot = (int)Mathf.Floor(Random.Range(2, AnimalData.Instance.Animals[whatSpecies].TickleSpots + 0.99f) - 2);
        newCurrentAnimal.FavoriteTickleSpot = favoriteTickleSpot;
        int detestableTickleSpot = -1;
        while (detestableTickleSpot == -1)
        {
            detestableTickleSpot = (int)Mathf.Floor(Random.Range(2, AnimalData.Instance.Animals[whatSpecies].TickleSpots + 0.99f) - 2);
            if (detestableTickleSpot == favoriteTickleSpot)
                detestableTickleSpot = -1;
        }
        newCurrentAnimal.DetestableTickleSpot = detestableTickleSpot;

        newCurrentAnimal.Satisfaction = AnimalData.Instance.Animals[whatSpecies].BaseHappyness * 10;
        newCurrentAnimal.Hapyness = AnimalData.Instance.Animals[whatSpecies].BaseHappyness;
        newCurrentAnimal.Breath = AnimalData.Instance.Animals[whatSpecies].BaseBreath;
        newCurrentAnimal.Anger = AnimalData.Instance.Animals[whatSpecies].BaseAnger;

        Debug.Log(favoriteTickleSpot + " " + detestableTickleSpot);
        return newCurrentAnimal;
    }

    void Awake()
    {
        Instance = this;
    }
}