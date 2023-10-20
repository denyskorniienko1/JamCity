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
    [SerializeField] private IntObservableVariableSO magazineCount;
    [SerializeField] private IntObservableVariableSO totalCount;
    [SerializeField] private float muzzleVelocity = 700f;
    [SerializeField] private float cooldownWindow = 0.1f;

    [SerializeField] private int defaultCapacity = 90;
    [SerializeField] private int maxSize = 180;

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
            defaultCapacity,
            maxSize);
        magazineCount.OnGameRun();
        totalCount.OnGameRun();
    }

    public void Shoot(Vector3 direction)
    {
        if (Time.time > nextTimeToShoot && projectilePool != null && magazineCount.RuntimeValue > 0)
        {
            var bullet = projectilePool.Get();

            if (bullet == null)
                return;

            bullet.transform.SetPositionAndRotation(muzzlePosition.transform.position, Quaternion.identity);
            bullet.body.AddForce(direction.normalized * muzzleVelocity, ForceMode.Acceleration);

            muzzleVisualEffect.Play();
            magazineCount.RuntimeValue--;

            bullet.Deactivate();
            nextTimeToShoot = Time.time + cooldownWindow;
        }
    }

    public void Reload()
    {
        if(totalCount.RuntimeValue > 0)
        {
            
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
