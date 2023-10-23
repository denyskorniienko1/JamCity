using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int RoomId;

    private GameManagerM01 gameManager;
    private NavMeshAgent agent;
    private Animator anim;
    private GameObject player;
    private bool isDead = false;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerM01>();
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("PlayerSeeking", 1f, 0.1f);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = agent.velocity;
        if (velocity != Vector3.zero)
            anim.SetBool("IsRunning", true);
        else
            anim.SetBool("IsRunning", false);

        if (player == null)
        {
            anim.SetBool("Attack", false);
            CancelInvoke();
        }
    }

    void PlayerSeeking()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 thisPos = this.transform.position;
        float distance = Vector3.Distance(playerPos, thisPos);
 
        if (gameManager.PlayerRoomId == RoomId && !isDead)
        {
            if(distance > 2)
            {
                agent.SetDestination(playerPos);
                agent.isStopped = false;
                anim.SetBool("Attack", false);
            }
            else
            {
                anim.SetBool("Attack", true);
                agent.isStopped = true;
            }
        }
        else if(!isDead)
        {
            agent.isStopped = true;
        }
    }

    public void OnDeath()
    {
        isDead = true;
    }
}
