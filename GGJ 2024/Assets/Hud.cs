using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    public static Hud Instance;

    [SerializeField] TMP_Text day, money, currentAnimal, animalSatisfation;

    public void UpdateDay()
    {
        day.text = "Day: " + GameplayManager.Instance.Day.ToString();
    }
    public void UpdateMoney()
    {
        money.text = "Money: " + GameplayManager.Instance.Money.ToString() + "$";
    }
    public void UpdateCurrentAnimal()
    {
        currentAnimal.text =  GameplayManager.Instance.CurrentAnimal.Name;
    }
    public void UpdateAnimalSatisfation()
    {
        animalSatisfation.text =  "Animal satisfation: " + ((int)GameplayManager.Instance.CurrentAnimal.Satisfaction).ToString() + "%";
    }

    void Awake()
    {
        Instance = this;
    }
}