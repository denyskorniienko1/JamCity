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

    [SerializeField] private float timeoutDelay = 3f;

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

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        DeactivateImmediately();
    }
}