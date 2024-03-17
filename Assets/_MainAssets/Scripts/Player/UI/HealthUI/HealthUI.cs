using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthSystem playerHS;

    [SerializeField] private Text playerTXT;

    private void Awake()
    {
        UpdateHealthText(playerHS.MaxHealth);
    }

    private void OnEnable()
    {
        playerHS.ReceivedDamage += UpdateHealthText;
    }

    private void OnDisable()
    {
        playerHS.ReceivedDamage -= UpdateHealthText;
    }

    private void OnDestroy()
    {
        playerHS.ReceivedDamage -= UpdateHealthText;
    }

    private void UpdateHealthText(float healthValue)
    {
        playerTXT.text = $"{healthValue} HP";
    }
}
