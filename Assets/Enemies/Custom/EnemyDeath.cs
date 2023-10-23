using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : MonoBehaviour
{
    public void OnDeath()
    {
        Animator anim = GetComponent<Animator>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        anim.SetBool("IsDead", true);
        agent.isStopped = true;
        Invoke("RIP", 5);
    }

    public void RIP()
    {
        Destroy(gameObject);
    }
}
