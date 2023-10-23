using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputPistolController : PlayerBaseInputWeaponController
{
    public override void Shoot(InputAction.CallbackContext context) => SingleShoot(context);

    public void SingleShoot(InputAction.CallbackContext context)
    {
        if (!CanShoot())
            return;

        if (context.started)
            weapon.Shoot(GetShootDirection());
    }
}
