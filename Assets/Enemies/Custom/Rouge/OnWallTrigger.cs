using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWallTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
