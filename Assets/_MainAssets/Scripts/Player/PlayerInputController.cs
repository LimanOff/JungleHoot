using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public static GameInput GameInput { get; private set; }
    private static PlayerInputController _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            GameInput = new GameInput();

            GameInput.Enable();
        }
        else if (_instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameInput.Enable();
    }
    private void OnDisable()
    {
        GameInput.Disable();
    }
}
