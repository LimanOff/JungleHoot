using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimerUI : MonoBehaviour
{
    public Text _timerText;
    private Timer _timer;

    [Inject]
    private void Construct(Timer timer)
    {
        _timer = timer;
    }

    private void Awake()
    {
        UpdateText();
        _timer.TimeTick += UpdateText;
    }
    private void OnDestroy()
    {
        _timer.TimeTick -= UpdateText;
    }

    private void UpdateText()
    {
        float minutes = _timer.CurrentTimeInSeconds / 60;
        float seconds = _timer.CurrentTimeInSeconds % 60;
        _timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
