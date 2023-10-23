using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{
    public UnityEvent<Collider> OnTriggerDetected;
    private bool canHit = true;

    public void OnTriggerEnter(Collider collider)
    {
        if (canHit && collider.tag.CompareTo("Player") == 0)
        {
            OnTriggerDetected.Invoke(collider);
            StartCoroutine(delayTrigger());
        }
    }

    IEnumerator delayTrigger()
    {
        canHit = false;
        yield return new WaitForSeconds(2);
        canHit = true;
    }
}
