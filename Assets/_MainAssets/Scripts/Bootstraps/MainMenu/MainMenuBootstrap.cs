using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuBootstrap : MonoBehaviour
{
    [SerializeField] private List<RebindPanel> _rebindPanels;
    [SerializeField] private MainMenuCanvasBootstrap _mainMenuCanvasBootstrap;

    private PlayerInputController _inputController;

    [Inject]
    private void Construct(PlayerInputController inputController)
    {
        _inputController = inputController;
    }

    private void Awake()
    {
        _rebindPanels.ForEach(rp => rp.Initialize(_inputController));
        _mainMenuCanvasBootstrap.Initialize();
    }
}
