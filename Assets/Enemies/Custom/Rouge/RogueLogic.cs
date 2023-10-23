using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RougeLogic : MonoBehaviour
{
    public GameObject crossbow;
    public GameObject projectilePrefab;
    public int RoomId;

    private GameManagerM01 gameManager;
    private float arrowVelocity = 600f;
    private NavMeshAgent agent;
    private Animator anim;
    private GameObject player;
    private bool isDead = false;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerM01>();
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("PlayerSeeking", 1.0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = agent.velocity;
        if (velocity != Vector3.zero)
            anim.SetBool("IsRunning", true);
        else
            anim.SetBool("IsRunning", false);

        
    }

    void PlayerSeeking()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.y = 1;
        Vector3 thisPos = this.transform.position;
        thisPos.y = 1;
        float distance = Vector3.Distance(playerPos, thisPos);
        if (gameManager.PlayerRoomId == RoomId && !isDead)
        {
            RaycastHit ray;
            bool hit = Physics.Raycast(thisPos, (playerPos - thisPos), out ray,  30);
            if (hit && ray.collider.transform == player.transform)
            {
                Vector3 targetPostition = new Vector3(player.transform.position.x,
                                       this.transform.position.y,
                                       player.transform.position.z);
                transform.LookAt(targetPostition);

                agent.isStopped = true;

                if (canShoot)
                {
                    anim.SetBool("Shot", true);
                    StartCoroutine(Shooting());
                } 
            }
            else
            {
                agent.SetDestination(playerPos);
                agent.isStopped = false;
                anim.SetBool("Shot", false);
            }
        }
        else if (!isDead)
        {
            agent.isStopped = true;
        }
    }

    public void OnDeath()
    {
        isDead = true;
    }

    public void Shot(Vector3 playerPos, Vector3 thisPos)
    {
        if (isDead) return;
        Vector3 direction = playerPos - thisPos;
        
        GameObject arrow = Instantiate(projectilePrefab);
        arrow.transform.position = crossbow.transform.position;
        arrow.transform.SetPositionAndRotation(crossbow.transform.position, transform.rotation);
        arrow.transform.Rotate(90,0,0);
        Rigidbody body = arrow.gameObject.GetComponent<Rigidbody>();
        body.AddForce(direction.normalized * arrowVelocity, ForceMode.Acceleration);
    }

    IEnumerator Shooting()
    {
        canShoot = false;
        yield return new WaitForSeconds(2.5f);
        Shot(player.transform.position, transform.position);
        canShoot = true;
    }
}
