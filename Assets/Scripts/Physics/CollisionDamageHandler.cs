using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageHandler : MonoBehaviour
{
    public FloatVariableSO Damage;

    public void Handle(Collision collision)
    {
        var health = collision.gameObject.GetComponent<Health>();
        if ( health != null)
        {
            health.Remove(Damage.InitialValue);
        }
    }
}