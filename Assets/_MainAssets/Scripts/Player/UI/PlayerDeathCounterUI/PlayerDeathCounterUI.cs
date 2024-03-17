using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathCounterUI : MonoBehaviour
{
    [SerializeField] private PlayerDeathCounter _playerDeathCounter;
    [SerializeField] private Text _counterTXT;

    private void Awake()
    {
        _playerDeathCounter.DeathCountChanged += UpdateDeathCounterText;
    }

    private void OnDestroy()
    {
        _playerDeathCounter.DeathCountChanged -= UpdateDeathCounterText;
    }

    private void UpdateDeathCounterText(int countToDisplay)
    {
        _counterTXT.text = $"{countToDisplay}";
    }
}
