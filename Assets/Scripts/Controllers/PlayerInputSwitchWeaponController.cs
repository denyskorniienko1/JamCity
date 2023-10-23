using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSwitchWeaponController : MonoBehaviour
{
    private SwitchInputActions switchInputActions;
    [SerializeField] private ShootWeapon currentWeapon;
    [SerializeField] private ShootWeapon[] weapons;

    [SerializeField] private int currentWeaponIndex;

    private void OnEnable()
    {
        switchInputActions = new SwitchInputActions();
        switchInputActions.SwitchMap.Switch.performed += Switch;
        switchInputActions.Enable();
    }

    public void Switch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var value = context.ReadValue<float>();

            if (value == 0)
                return;

            var sign = Mathf.Sign(value);
            currentWeapon.gameObject.SetActive(false);
            currentWeaponIndex += (int)sign;
            currentWeaponIndex = Mathf.Abs(currentWeaponIndex % weapons.Length);
            currentWeapon = weapons[currentWeaponIndex];
            currentWeapon.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        switchInputActions.SwitchMap.Switch.performed -= Switch;
        switchInputActions.Disable();
    }
}