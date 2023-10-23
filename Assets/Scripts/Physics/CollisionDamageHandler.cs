using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageHandler : MonoBehaviour
{
    public FloatVariableSO Damage;

    public void Handle(Collision collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();
        if ( health != null)
        {
            health.Remove(Damage.InitialValue);
        }

    }
    public void Handle(Collider collider)
    {
        var health = collider.gameObject.GetComponent<HealthBase>();
        if (health != null && collider.tag == "Player")
        {
            health.Remove(Damage.InitialValue);
        }
    }
}