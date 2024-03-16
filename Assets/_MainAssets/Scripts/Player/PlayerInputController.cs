using UnityEngine;

public class PlayerInputController
{
    public GameInput GameInput { get; private set; }

    public PlayerInputController()
    {
        GameInput = new GameInput();
        GameInput.Enable();
    }

    ~PlayerInputController()
    {
        GameInput.Disable();
    }
}
