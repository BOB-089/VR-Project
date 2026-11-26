using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private int money;

    public static MoneyManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        UpdateMoney();
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount) { return false; }
        money -= amount;
        UpdateMoney();
        return true;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        moneyText.text = $"${money}";
    }
}
