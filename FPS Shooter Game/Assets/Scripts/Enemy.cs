using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public int health = 100;
    public Material hitMat;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public GameObject weaponFlash;
    public float fireRate;
    public float lastFireTime = 0f;
    public float bloom;
    public AudioClip shootingSFX;


    private Rigidbody rb;
    private Renderer rend;
    private Material originalMaterial;

    private NavMeshAgent agent;
    // AI Settings
    public int currentPointIndex = 0;
    public Vector3 currentTarget;
    public float positionThreshold;
    public float idleTime = 5f;
    public float attackDistance = 5f;
    public float maxVisionDistance = 20f;
    public float minimalChasingHealth = 30f;

    public Transform[] patrolPoints;
    private float idleTimeCounter;
    private Transform playerTransform;
    private bool canSeePlayer;
    private Vector3 lastKnownPlayerPosition;

    public enum State { Idle, Patrolling, Chasing, Attacking }
    public State state = State.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;

        agent = GetComponent<NavMeshAgent>();
        //if (!agent.isOnNavMesh) {
        //    Debug.Log("agent is not anvamesh");
        //    return;
        //}
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
        else
        {
            Debug.LogError("Enemy is NOT near NavMesh!");
        }


        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        GameObject patrolPointParent = GameObject.FindWithTag("PatrolPoint");
        patrolPoints = patrolPointParent.GetComponentsInChildren<Transform>().Where(t => t != patrolPointParent.transform).ToArray();
    }

    private void Update()
    {
        LookForPlayer();
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrolling:
                Patrolling();
                break;
            case State.Chasing:
                Chasing();
                break;
            case State.Attacking:
                Attacking();
                break;
        }
        rb.linearVelocity = Vector3.zero;

        LookAtPlayer();
        SetLastKnownPlayerPosition();
    }

    private void Idle() {
        agent.ResetPath();

        idleTimeCounter -= Time.deltaTime;

        if (idleTimeCounter <= 0)
        {
            state = State.Patrolling;
            idleTimeCounter = idleTime;
        }
    }

    private void Patrolling() {
        if (Vector3.Distance(currentTarget,transform.position) < positionThreshold)
        {
            float chance = Random.Range(0, 100);

            if (chance < 10)
            {
                state = State.Idle;
                return;
            }
            currentPointIndex++;
            currentTarget = patrolPoints[currentPointIndex % patrolPoints.Length].position;
        } else
        {
            agent.SetDestination(currentTarget);
        }
    }

    private void Chasing() {
        idleTimeCounter = idleTime;
        agent.SetDestination(lastKnownPlayerPosition);

        if ( health < minimalChasingHealth)
        {
            state = State.Patrolling;
        }
        else if (Vector3.Distance(transform.position, playerTransform.position) < attackDistance && canSeePlayer)
        {
            state = State.Attacking;
        } else if (Vector3.Distance(transform.position,playerTransform.position) > maxVisionDistance)
        {
            state = State.Patrolling;
        } else if (Vector3.Distance(transform.position, playerTransform.position) < positionThreshold && !canSeePlayer)
        {
            state = State.Patrolling;
        }
    }

    private void Attacking() {
        idleTimeCounter = idleTime;
        agent.ResetPath();

        Shoot();

        if (Vector3.Distance(transform.position,playerTransform.position) > attackDistance || !canSeePlayer)
        {
            if (health < minimalChasingHealth)
            {
                state = State.Patrolling;
            } else
            {
                state = State.Chasing;
            }
        }
    }

    private void LookForPlayer() {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, maxVisionDistance))
        {
            canSeePlayer = hit.transform == playerTransform;
        }

        if (canSeePlayer && state != State.Attacking)
        {
            state = State.Chasing;
        }
    }

    private void LookAtPlayer() {
        if (canSeePlayer)
        {
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        }
    }

    private void SetLastKnownPlayerPosition()
    {
        if (canSeePlayer)
        {
            lastKnownPlayerPosition = playerTransform.position;
        }
    }



void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            health -= 10;
            StartCoroutine(Blink());

        }
        if ( collision.gameObject.tag == "GlockDamage")
        {
            health -= 5;
            StartCoroutine(Blink());

        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        //if (!this.enabled) return;
        //rb.freezeRotation = false;
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 5f);
    }

    IEnumerator Blink()
    {
        rend.material = hitMat;
        yield return new WaitForSeconds(0.1f);
        rend.material = originalMaterial;
    }

    private void Shoot()
    {
        if (Time.time > lastFireTime + fireRate)
        {

            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.Normalize();

            Quaternion bulletRotation = Quaternion.LookRotation(directionToPlayer);

            float maxInaccuracy = 10f;
            float currentInaccuracy = bloom * maxInaccuracy;
            float randomJaw = Random.Range(-currentInaccuracy, currentInaccuracy);
            float randomPitch = Random.Range(-currentInaccuracy, currentInaccuracy);


            bulletRotation *= Quaternion.Euler(randomPitch, randomJaw + 90, 0f);


            AudioManager.Instance.PlaySFX(shootingSFX,0.5f);

            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);
            Instantiate(weaponFlash, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            lastFireTime = Time.time;
        }

            

    }

}
