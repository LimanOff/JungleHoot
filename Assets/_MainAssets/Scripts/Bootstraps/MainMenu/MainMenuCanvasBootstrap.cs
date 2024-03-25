using ModestTree;
using UnityEngine;

public class MainMenuCanvasBootstrap : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _audioSettingsPanel;
    [SerializeField] private GameObject _inputSettingsPanel;

    public void Initialize()
    {
        ValidatePanels();
        _mainPanel.SetActive(true);
        _settingsPanel.SetActive(false);
        _audioSettingsPanel.SetActive(false);
        _inputSettingsPanel.SetActive(false);
    }

    public void OpenAllPanels()
    {
        _mainPanel.SetActive(true);
        _settingsPanel.SetActive(true);
        _audioSettingsPanel.SetActive(true);
        _inputSettingsPanel.SetActive(true);
    }

    private void ValidatePanels()
    {
        Assert.IsNotNull(_mainPanel,"(MainMenuCanvasBootstrap/ValidatePanels) Не задан MainPanel");
        Assert.IsNotNull(_settingsPanel, "(MainMenuCanvasBootstrap/ValidatePanels) Не задан SettingsPanel");
        Assert.IsNotNull(_audioSettingsPanel, "(MainMenuCanvasBootstrap/ValidatePanels) Не задан AudioSettingsPanel");
        Assert.IsNotNull(_inputSettingsPanel, "(MainMenuCanvasBootstrap/ValidatePanels) Не задан InputSettingsPanel");
    }
}
