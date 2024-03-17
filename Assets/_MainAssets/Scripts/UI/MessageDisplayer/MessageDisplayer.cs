using UnityEngine;

public class MessageDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _messagePrefab;
    private Message _message;
    [field: SerializeField] public int ShowTime { get; private set; }
    [field: SerializeField] public int MaxCountOfMessages { get; private set; }
    private int _currentCountOfMessages;

    [SerializeField] private Color _colorOfText;

    public void ShowInfoMessage(string messageText)
    {
        _colorOfText.a = 1;

        if (_currentCountOfMessages < MaxCountOfMessages)
        {
            _message = Instantiate(_messagePrefab, _parent.transform).GetComponent<Message>();
            _message.Initialize(messageText, ShowTime, _colorOfText);

            _currentCountOfMessages++;
            _message.TimeUp += () =>
            {
                _currentCountOfMessages--;
                _message.TimeUp -= () => _currentCountOfMessages--;
            };
        }
    }

    [ContextMenu("ShowInfoMessage (TEST)")]
    public void ShowInfoMessage()
    {
        _message = Instantiate(_messagePrefab, _parent.transform).GetComponent<Message>();
        _message.Initialize("TEST", ShowTime, _colorOfText);
    }
}
