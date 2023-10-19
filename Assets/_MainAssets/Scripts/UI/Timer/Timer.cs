using System;
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static Action TimeUp;

    private bool _canCount;

    private DateTime _time;
    public DateTime Time
    {
        get => _time;
        private set
        {
            if(value.ToString("mm:ss") == "59:59")
            {
                TimeUp?.Invoke();
                _canCount = false;
            }
            else
            {
                _time = value;
            }
        }
    }

    public void Initialize()
    {
        Time = new DateTime(2000, 11, 11, 1, 00, 20);
        _canCount = true;
    }

    private IEnumerator Start()
    {
        while(_canCount)
        {
            Time = Time.AddSeconds(-1);

            yield return new WaitForSeconds(1f);
        }
    }
}
