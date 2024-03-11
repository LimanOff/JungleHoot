using System;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public event Action TimeUp;
    [SerializeField] private Text _textUI;

    public void Initialize(string messageText, int showTime, Color color)
    {
        _textUI = GetComponent<Text>();
        _textUI.color = color;

        _textUI.CrossFadeAlpha(0f, showTime, false);
    }

    private void Update()
    {
        if (_textUI.canvasRenderer.GetAlpha() == 0)
        {
            TimeUp?.Invoke();
            Destroy(gameObject);
        }
    }
}
