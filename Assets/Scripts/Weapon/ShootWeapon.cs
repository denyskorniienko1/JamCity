using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;
using static Unity.VisualScripting.Member;

public class ShootWeapon : MonoBehaviour
{
    public Transform muzzlePosition;

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private WeaponSO weapon;

    [SerializeField] private VisualEffect muzzleVisualEffect;

    // throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool collectionCheck = true;

    private ObjectPool<Projectile> projectilePool;
    private bool isShooting;
    private float nextTimeToShoot;

    // Start is called before the first frame update
    void Start()
    {
        projectilePool = new ObjectPool<Projectile>(CreateProjectile,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            collectionCheck,
            weapon.BulletPoolDefaultCapacity,
            weapon.BulletPoolMaxSize);
        weapon.MagazineCount.OnGameRun();
        weapon.TotalCount.OnGameRun();
    }

    public void Shoot(Vector3 direction)
    {
        if (Time.time > nextTimeToShoot && projectilePool != null && weapon.MagazineCount.RuntimeValue > 0)
        {
            var bullet = projectilePool.Get();

            if (bullet == null)
                return;

            bullet.transform.SetPositionAndRotation(muzzlePosition.transform.position, Quaternion.identity);
            bullet.body.AddForce(direction.normalized * weapon.MuzzleVelocity, ForceMode.Acceleration);

            muzzleVisualEffect.Play();
            weapon.MagazineCount.RuntimeValue--;

            bullet.Deactivate();
            nextTimeToShoot = Time.time + weapon.CooldownWindow;
        }
    }

    public void Reload()
    {
        if(weapon.TotalCount.RuntimeValue > 0 && weapon.MagazineCount.RuntimeValue < weapon.MagazineCount.MaxValue)
        {
            var neededToAdd = weapon.MagazineCount.MaxValue - weapon.MagazineCount.RuntimeValue;

            if(neededToAdd <= weapon.TotalCount.RuntimeValue) 
            {
                weapon.MagazineCount.RuntimeValue = weapon.MagazineCount.RuntimeValue + neededToAdd;
                weapon.TotalCount.RuntimeValue = weapon.TotalCount.RuntimeValue - neededToAdd;
            }
            else
            {
                weapon.MagazineCount.RuntimeValue = weapon.TotalCount.RuntimeValue;
                weapon.TotalCount.RuntimeValue = 0;
            }
        }
    }

    // invoked when creating an item to populate the object pool
    private Projectile CreateProjectile()
    {
        Projectile projectileInstance = Instantiate(projectilePrefab);
        projectileInstance.pool = projectilePool;
        return projectileInstance;
    }

    // invoked when returning an item to the object pool
    private void OnReleaseToPool(Projectile pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(Projectile pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(Projectile pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
}
