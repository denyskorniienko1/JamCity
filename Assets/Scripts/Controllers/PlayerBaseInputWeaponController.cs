using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseInputWeaponController : MonoBehaviour
{
    private WeaponInputActions weaponInputActions;

    [SerializeField] protected ShootWeapon weapon;

    [SerializeField] private float MinShootDistance = 2;
    [SerializeField] private float distanceToMousePoint;
    [SerializeField] private float angle;

    private void OnEnable()
    {
        weaponInputActions = new WeaponInputActions();
        weaponInputActions.Weapon.FireStart.started += Shoot;
        weaponInputActions.Weapon.FireStart.performed += Shoot;
        weaponInputActions.Weapon.FireStart.canceled += Shoot;
        weaponInputActions.Weapon.Reload.started += Reload;
        weaponInputActions.Enable();
    }

    private void OnDisable()
    {
        Disable();
    }

    public void Disable()
    {
        if (weaponInputActions == null) return;

        weaponInputActions.Weapon.FireStart.started -= Shoot;
        weaponInputActions.Weapon.FireStart.performed -= Shoot;
        weaponInputActions.Weapon.FireStart.canceled -= Shoot;
        weaponInputActions.Weapon.Reload.started -= Reload;
        weaponInputActions.Disable();
    }

    public abstract void Shoot(InputAction.CallbackContext context);

    public void Reload(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            weapon.Reload();
        }
    }

    protected Vector3 GetShootDirection()
    {
        var muzzleGlobalPosition = weapon.muzzlePosition.transform.TransformPoint(Vector3.zero);
        var mousePosition = MouseUtills.GetMousePositionInTheWorld(Camera.main, muzzleGlobalPosition.y);
        var shootDirection = mousePosition - muzzleGlobalPosition;

        return shootDirection;
    }

    protected bool CanShoot()
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