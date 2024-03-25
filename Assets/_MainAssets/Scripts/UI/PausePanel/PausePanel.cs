using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PausePanel : MonoBehaviour
{
    private PlayerInputController _inputController;

    [Inject]
    private void Construct(PlayerInputController inputController)
    {
        _inputController = inputController;
    }

    private void Awake()
    {
        _inputController.GameInput.UI.CallPausePanel.performed += OnCallPausePanel;
    }

    private void OnDestroy()
    {
        _inputController.GameInput.UI.CallPausePanel.performed -= OnCallPausePanel;
    }

    private void OnCallPausePanel(InputAction.CallbackContext context)
    {
        if (gameObject.activeSelf)
            ClosePanel();
        else
            OpenPanel();
    }

    private void OpenPanel()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ClosePanel(GameObject PausePanel)
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
