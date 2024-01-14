using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathCounterUI : MonoBehaviour
{
    [SerializeField] private PlayerDeathCounter _playerDeathCounter;
    [SerializeField] private Text _counterTXT;

    private void OnEnable()
    {
        _playerDeathCounter.DeathCounterUpdated += DisplayDeathCounter;
    }

    private void OnDisable()
    {
        _playerDeathCounter.DeathCounterUpdated -= DisplayDeathCounter;
    }

    private void DisplayDeathCounter(int countToDisplay)
    {
        _counterTXT.text = $"{countToDisplay}";
    }
}
