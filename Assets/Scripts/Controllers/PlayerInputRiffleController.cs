using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputRiffleController : PlayerBaseInputWeaponController
{
    private bool burstShootingMode;

    public override void Shoot(InputAction.CallbackContext context) => BurstShoot(context);

    private void BurstShoot(InputAction.CallbackContext context)
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
            if (!CanShoot())
            {
                yield return null;
                continue;
            }

            weapon.Shoot(GetShootDirection());
            yield return null;
        }
    }

}
