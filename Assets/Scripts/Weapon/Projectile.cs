using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public Rigidbody body;
    public ObjectPool<Projectile> pool;
    public TrailRenderer trailRenderer;

    [SerializeField] private float timeoutDelay = 3f;

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // reset the moving Rigidbody
        body.velocity = new Vector3(0f, 0f, 0f);
        body.angularVelocity = new Vector3(0f, 0f, 0f);
        trailRenderer.Clear();

        // release the projectile back to the pool
        pool.Release(this);
    }
}