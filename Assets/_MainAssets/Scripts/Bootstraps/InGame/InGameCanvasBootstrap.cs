using ModestTree;
using UnityEngine;

public class InGameCanvasBootstrap : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _roundResultPanel;
    [SerializeField] private GameObject _pausePanel;

    [Header("Settings Panel")]
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _inputSettingsPanel;
    [SerializeField] private GameObject _audioSettingsPanel;

    public void Initialize()
    {
        ValidatePanels();
        
        _settingsPanel.SetActive(true);
        _inputSettingsPanel.SetActive(false);
        _audioSettingsPanel.SetActive(false);

        _gamePanel.SetActive(true);
        _roundResultPanel.SetActive(false);
        _pausePanel.SetActive(false);
    }

    public void OpenAllPanels()
    {
        _gamePanel.SetActive(true);
        _roundResultPanel.SetActive(true);
        _pausePanel.SetActive(true);

        _settingsPanel.SetActive(true);
        _inputSettingsPanel.SetActive(true);
        _audioSettingsPanel.SetActive(true);
    }

    private void ValidatePanels()
    {
        Assert.IsNotNull(_gamePanel, "(MainMenuCanvasBootstrap/ValidatePanels) �� ����� GamePanel");
        Assert.IsNotNull(_gamePanel, "(MainMenuCanvasBootstrap/ValidatePanels) �� ����� RoundResultPanel");
        Assert.IsNotNull(_gamePanel, "(MainMenuCanvasBootstrap/ValidatePanels) �� ����� PausePanel");

        Assert.IsNotNull(_settingsPanel, "(MainMenuCanvasBootstrap/ValidatePanels) �� ����� SettingsPanel");
        Assert.IsNotNull(_inputSettingsPanel, "(MainMenuCanvasBootstrap/ValidatePanels) �� ����� InputSettingsPanel");
        Assert.IsNotNull(_audioSettingsPanel, "(MainMenuCanvasBootstrap/ValidatePanels) �� ����� AudioSettingsInput");
    }
}
