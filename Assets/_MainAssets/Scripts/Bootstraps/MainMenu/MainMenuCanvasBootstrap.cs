using UnityEngine;

public class MainMenuCanvasBootstrap : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject AudioSettingsPanel;
    [SerializeField] private GameObject InputSettingsPanel;

    public void Initialize()
    {
        MainPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        AudioSettingsPanel.SetActive(false);
        InputSettingsPanel.SetActive(false);
    }
}
