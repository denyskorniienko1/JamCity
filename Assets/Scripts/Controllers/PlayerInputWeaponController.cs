using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWeaponController : MonoBehaviour
{
    [SerializeField] private ShootWeapon weapon;

    private bool burstShootingMode;

    public void SingleShoot(InputAction.CallbackContext context)
    {
        if (context.started)
            weapon.Shoot();
    }

    public void BurstShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            burstShootingMode = true;
            StartCoroutine(StartBurstShooting());
        }
        else if (context.canceled)
        {
            burstShootingMode = false;
        }
    }

    private IEnumerator StartBurstShooting()
    {
        while (burstShootingMode)
        {
            weapon.Shoot();
            yield return null;
        }
    }
}
