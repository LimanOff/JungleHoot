using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RoundResults : MonoBehaviour
{
    private LoaderNewLevels _loaderNewLevels;

    [SerializeField] private Timer _timer;
    [Space]
    [SerializeField] private PlayerDeathCounter p1DeathCounter;
    [SerializeField] private PlayerDeathCounter p2DeathCounter;
    [Space]
    [SerializeField] private GameObject _roundResultsPanel;
    [SerializeField] private Text _summaryText;
    [SerializeField] private Button _loadNewLevelButton;

    [Inject]
    private void Construct(LoaderNewLevels loaderNewLevels)
    {
        _loaderNewLevels = loaderNewLevels;
    }

    private void OnEnable()
    {
        _timer.TimeUp += Summarize;
        _loadNewLevelButton.onClick.AddListener(delegate { _loaderNewLevels.LoadNewLevel(); });
    }

    private void OnDisable()
    {
        _timer.TimeUp -= Summarize;
        _loadNewLevelButton.onClick.RemoveListener(delegate { _loaderNewLevels.LoadNewLevel(); });
    }

    private void Summarize()
    {
        _roundResultsPanel.SetActive(true);

        if (p1DeathCounter.DeathCount == p2DeathCounter.DeathCount)
        {
            _summaryText.text = "Ничья";
            _summaryText.color = Color.yellow;
        }

        if (p1DeathCounter.DeathCount < p2DeathCounter.DeathCount)
        {
            _summaryText.text = _summaryText.text.Replace('N', '1');
        }
        else
        {
            _summaryText.text = _summaryText.text.Replace('N', '2');
        }
    }
}
