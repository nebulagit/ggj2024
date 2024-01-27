using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    public static Hud Instance;

    [SerializeField] TMP_Text day, money, currentAnimal, animalEnjoying, animalIrritation, animalBreathing, time;

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
    public void UpdateAnimalStatus()
    {
        animalEnjoying.text =  "Animal enjoying: " + ((int)GameplayManager.Instance.CurrentAnimal.Enjoying).ToString() + "%";
        animalIrritation.text =  "Animal irritation: " + ((int)GameplayManager.Instance.CurrentAnimal.Irritation).ToString() + "%";
        animalBreathing.text =  "Animal breathing: " + ((int)GameplayManager.Instance.CurrentAnimal.Breathing).ToString() + "%";
    }
    public void UpdateTime()
    {
        time.text =  "Time: " + GameplayManager.Instance.TimeSec.ToString();
    }

    void Awake()
    {
        Instance = this;
    }
}