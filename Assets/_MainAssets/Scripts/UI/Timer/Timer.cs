using System;
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static Action TimeUp;

    [Range(30f,180f), Tooltip("Время раунда в секундах (от 30 сек до 3 мин)")]
    public float StartTimeInSeconds;

    public float CurrentTimeInSeconds { get; private set; }

    public Coroutine Counting;

    public void Initialize()
    {
        CurrentTimeInSeconds = StartTimeInSeconds;
        Counting = StartCoroutine(Count());

        TimeUp += delegate { StopCoroutine(Counting); };
    }

    private void OnDisable()
    {
        TimeUp -= delegate { StopCoroutine(Counting); };
    }

    private IEnumerator Count()
    {
        while (CurrentTimeInSeconds != 0)
        {
            CurrentTimeInSeconds--;
            yield return new WaitForSeconds(1f);
        }
        TimeUp?.Invoke();
    }
}
