using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

public class Impact : MonoBehaviour
{
    public ObjectPool<Impact> pool;
    public VisualEffect effect;

    public void Deactivate()
    { 
        
        StartCoroutine(StartDeactivateRoutine());
    }

    private IEnumerator StartDeactivateRoutine()
    {
        yield return new WaitForSeconds(2);

        DeactivateImmediately();
    }

    public void DeactivateImmediately()
    {
        // reset the moving Rigidbody
        transform.position = new Vector3(0f, 0f, 0f);

        // release the projectile back to the pool
        pool.Release(this);
    }
}