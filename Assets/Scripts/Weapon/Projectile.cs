using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public Rigidbody body;
    public ObjectPool<Projectile> pool;
    public TrailRenderer trailRenderer;
    public MeshRenderer meshRenderer;
    public Collider collider;
    public Impact sparkPrefab;
    private ObjectPool<Impact> projectilePool;

    [SerializeField] private float timeoutDelay = 3f;

    private void Awake()
    {
        projectilePool = new ObjectPool<Impact>(CreateProjectile,
           OnGetFromPool,
           OnReleaseToPool,
           OnDestroyPooledObject,
           true,
           8,
           16);
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    public void DeactivateImmediately()
    {
        // reset the moving Rigidbody
        body.velocity = new Vector3(0f, 0f, 0f);
        body.angularVelocity = new Vector3(0f, 0f, 0f);
        trailRenderer.Clear();
        meshRenderer.enabled = true;
        collider.enabled = true;

        // release the projectile back to the pool
        pool.Release(this);
    }

    public void StopFlight()
    {
        body.velocity = Vector3.zero;
        meshRenderer.enabled = false;
        collider.enabled = false;
    }

    public void SpawnImpactFx(Collision collision)
    {
        var impact = projectilePool.Get();
        var contact = collision.GetContact(0);
        impact.transform.forward = contact.normal;
        impact.transform.position = contact.point;
        impact.effect.Play();
        //impact.Deactivate();
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        DeactivateImmediately();
    }

    private void OnReleaseToPool(Impact pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(Impact pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(Impact pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    private Impact CreateProjectile()
    {
        Impact projectileInstance = Instantiate(sparkPrefab);
        projectileInstance.pool = projectilePool;
        return projectileInstance;
    }
}