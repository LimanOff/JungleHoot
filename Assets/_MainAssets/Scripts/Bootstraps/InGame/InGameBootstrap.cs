using ModestTree;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InGameBootstrap : MonoBehaviour
{
    [SerializeField] private List<RebindPanel> _rebindPanels;
    [SerializeField] private InGameCanvasBootstrap _gameCanvasBootstrap;

    private PlayerInputController _inputController;

    [Inject]
    private void Construct(PlayerInputController inputController)
    {
        _inputController = inputController;
    }


    private void Awake()
    {
        ValidateComponents();

        _gameCanvasBootstrap.OpenAllPanels();
        _rebindPanels.ForEach(rp => rp.Initialize(_inputController));
        _gameCanvasBootstrap.Initialize();
    }

    private void ValidateComponents()
    {
        Assert.IsNotNull(_gameCanvasBootstrap,"(InGameBootstrap/ValidateComponents) Не задан GameCanvasBootstrap");
        Assert.IsNotNull(_rebindPanels, "(InGameBootstrap/ValidateComponents) Не задан список RebindPanels");
    }
}
