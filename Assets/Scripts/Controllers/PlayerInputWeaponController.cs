using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWeaponController : MonoBehaviour
{
    [SerializeField] private ShootWeapon weapon;

    [SerializeField] private float MinShootDistance = 2;
    [SerializeField] private float distanceToMousePoint;
    [SerializeField] private float angle;
    private bool burstShootingMode;


    public void SingleShoot(InputAction.CallbackContext context)
    {
        if (!CanShoot())
            return;

        if (context.started)
            weapon.Shoot(GetShootDirection());
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

    public void Reload(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            weapon.Reload();
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

    private Vector3 GetShootDirection()
    {
        var muzzleGlobalPosition = weapon.muzzlePosition.transform.TransformPoint(Vector3.zero);
        var mousePosition = MouseUtills.GetMousePositionInTheWorld(Camera.main, muzzleGlobalPosition.y);
        var shootDirection = mousePosition - muzzleGlobalPosition;

        return shootDirection;
    }

    private bool CanShoot()
    {
        var mousePosition = MouseUtills.GetMousePositionInTheWorld(Camera.main, transform.position.y);
        var directionToMouse = mousePosition - transform.position;

        Debug.DrawRay(transform.TransformPoint(Vector3.zero), transform.forward, UnityEngine.Color.cyan);
        Debug.DrawRay(transform.TransformPoint(Vector3.zero), directionToMouse, UnityEngine.Color.magenta);

        angle = Vector3.SignedAngle(transform.forward, directionToMouse, Vector3.up);
        if (angle < -45 || angle > 45)
            return false;

        distanceToMousePoint = Vector3.Distance(mousePosition, transform.position);

        return distanceToMousePoint >= MinShootDistance;
    }
}
