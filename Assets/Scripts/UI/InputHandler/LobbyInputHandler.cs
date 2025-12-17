using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyInputHandler : MonoBehaviour
{
    [SerializeField] private LobbyView _lobbyView;

    private GameInputActions _input;

    private void Awake()
    {
        _input = new GameInputActions();
    }

    private void OnEnable()
    {
        _input.Lobby.Enable();

        _input.Lobby.Navigate.performed += OnNavigate;
        _input.Lobby.SwitchPanel.performed += OnSwitchPanel;
        _input.Lobby.Confirm.performed += OnConfirm;
        _input.Lobby.Cancel.performed += OnCancel;
    }

    private void OnDisable()
    {
        _input.Lobby.Navigate.performed -= OnNavigate;
        _input.Lobby.SwitchPanel.performed -= OnSwitchPanel;
        _input.Lobby.Confirm.performed -= OnConfirm;
        _input.Lobby.Cancel.performed -= OnCancel;

        _input.Lobby.Disable();
    }

    private void OnNavigate(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValue<Vector2>();
        if (value.y is var y && y != 0)
            _lobbyView.OnNavigate(y > 0 ? -1 : 1);
    }

    private void OnSwitchPanel(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValue<Vector2>();
        if (value.x is var x && x != 0)
            _lobbyView.OnSwitchPanel(x > 0 ? 1 : -1);
    }

    private void OnConfirm(InputAction.CallbackContext ctx)
    {
        _lobbyView.OnConfirm();
    }

    private void OnCancel(InputAction.CallbackContext ctx)
    {
        _lobbyView.OnCancel();
    }
}
