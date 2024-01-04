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

    private void Update()
    {
        float minutes = Mathf.FloorToInt(_timer.CurrentTimeInSeconds / 60);
        float seconds = Mathf.FloorToInt(_timer.CurrentTimeInSeconds % 60);
        _timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
