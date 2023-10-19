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
        _timerText.text = _timer.Time.ToString("mm:ss");
    }
}
