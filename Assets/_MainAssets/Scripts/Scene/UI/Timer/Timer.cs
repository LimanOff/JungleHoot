using System;
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public event Action TimeUp;
    public event Action TimeTick;

    [Range(30f,180f), Tooltip("����� ������ � �������� (�� 30 ��� �� 3 ���)")]
    public float StartTimeInSeconds;

    public float CurrentTimeInSeconds { get; private set; }

    private Coroutine _counting;

    public void Initialize()
    {
        CurrentTimeInSeconds = StartTimeInSeconds;
        _counting = StartCoroutine(Count());

        TimeUp += delegate { StopCoroutine(_counting); };
    }

    private void OnDisable()
    {
        TimeUp -= delegate { StopCoroutine(_counting); };
    }

    private IEnumerator Count()
    {
        while (CurrentTimeInSeconds != 0)
        {
            CurrentTimeInSeconds--;
            TimeTick?.Invoke();
            yield return new WaitForSeconds(1f);
        }
        TimeUp?.Invoke();
    }
}
