using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{

    InputSystem_Actions _input;
    bool _isMenuOpen = false;

    private void OnEnable()
    {
        _input = new InputSystem_Actions();
        _input.Enable();
        _input.Player.Enable();
        _input.Player.MainMenu.performed += MainMenu_performed;
        _input.UI.ExitMenu.performed += ExitMenu_performed;
    }

    private void MainMenu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Setting Menu Pressed");
        if (!_isMenuOpen)
        {
            _isMenuOpen = true;
            UIManager.Instance.ActivateSettingsMenu(_isMenuOpen);
            _input.Player.Disable();
            _input.UI.Enable();
            GameManager.Instance.MenuActive(true);
        }
    }

    private void ExitMenu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_isMenuOpen)
        {
            _isMenuOpen = false;
            UIManager.Instance.ActivateSettingsMenu(_isMenuOpen);
            _input.UI.Disable();
            _input.Player.Enable();
            GameManager.Instance.MenuActive(false);
        }
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Disable();
    }
}
