using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public Rigidbody body;
    public TrailRenderer trailRenderer;
    public MeshRenderer meshRenderer;
    public Collider collider;

    private float timeoutDelay = 3f;
    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    public void DeactivateImmediately()
    {
        // reset the moving Rigidbody
        body.velocity = new Vector3(0f, 0f, 0f);
        body.angularVelocity = new Vector3(0f, 0f, 0f);
        //trailRenderer.Clear();
        meshRenderer.enabled = true;
        collider.enabled = true;

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
