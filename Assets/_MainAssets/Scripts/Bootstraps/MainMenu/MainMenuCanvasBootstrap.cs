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
        Assert.IsNotNull(_mainPanel,"(MainMenuCanvasBootstrap/ValidatePanels) �� ����� MainPanel");
        Assert.IsNotNull(_settingsPanel, "(MainMenuCanvasBootstrap/ValidatePanels) �� ����� SettingsPanel");
        Assert.IsNotNull(_audioSettingsPanel, "(MainMenuCanvasBootstrap/ValidatePanels) �� ����� AudioSettingsPanel");
        Assert.IsNotNull(_inputSettingsPanel, "(MainMenuCanvasBootstrap/ValidatePanels) �� ����� InputSettingsPanel");
    }
}
