using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthSystem playerHS;

    [SerializeField] private Text playerTXT;

    private void OnEnable()
    {
        playerHS.ReceivedDamage += UpdateHealth;
    }

    private void OnDisable()
    {
        playerHS.ReceivedDamage -= UpdateHealth;
    }

    private void UpdateHealth(float healthValue)
    {
        playerTXT.text = $"{healthValue} HP";
    }
}
