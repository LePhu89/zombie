using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    [Header("Zombie Things")]
    [SerializeField] private NavMeshAgent zombieAgent;
    [SerializeField] private Transform lookPoint;
    [SerializeField] private Camera attackingRaycastArea;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Animator anim;
    [SerializeField] private CapsuleCollider col;

    [Header("Zombie Guarding var")]
    [SerializeField] private GameObject[] walkPoints;
    int currentZombiePosition = 0;
    [SerializeField] private float zombieSpeed;
    float walkingPointRadius = 2f;

    [Header("Zombie state")]
    [SerializeField] private float visionRadius;
    [SerializeField] private float attackingRadius;
    [SerializeField] private bool playerInvisionRadius;
    [SerializeField] private bool playerInattackingRadius;

    [Header("Zombie Damage and Health")]
    [SerializeField] private float giveDamage = 5;
    [SerializeField] private float zombieHealth = 50;
    [SerializeField] private float presentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Player player;

    [Header("Zombie Attacking")]
    [SerializeField] private float timeAttack;
    private bool previousAttack;
    private void Awake()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
        presentHealth = zombieHealth;
        healthBar.GiveFullHealth(zombieHealth);
        col = GetComponent<CapsuleCollider>();

        if (playerBody == null)
        {
            playerBody = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (lookPoint == null)
        {
            lookPoint = GameObject.FindGameObjectWithTag("PlayerLookPoint").transform;
        }
    }
    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, playerLayer);

            if (!playerInvisionRadius && !playerInattackingRadius) Guard();       
            if (playerInvisionRadius && !playerInattackingRadius) Pursueplayer();
            if (playerInvisionRadius && playerInattackingRadius) AttackPlayer();
        
        
    }
    private void Guard()
    {
        if (walkPoints == null || walkPoints.Length == 0) return;

        if (Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentZombiePosition = Random.Range(0, walkPoints.Length);
            if (currentZombiePosition >= walkPoints.Length)
            {
                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards
                (transform.position, walkPoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);
        transform.LookAt(walkPoints[currentZombiePosition].transform.position);
        anim.SetBool("walking", true);
    }
    private void Pursueplayer()
    {
        
        if (zombieAgent.SetDestination(playerBody.position))
        {
            transform.LookAt(lookPoint);
            anim.SetBool("walking", false);
            anim.SetBool("run", true);
            anim.SetBool("attack", false);
            anim.SetBool("die", false);

        }
        else
        {
            anim.SetBool("walking", true);
            anim.SetBool("run", false);
            anim.SetBool("attack", false);
            anim.SetBool("die", true);

        }
        
    }
    private void AttackPlayer()
    {
        anim.SetBool("attack", true);
        anim.SetBool("run", false);
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(lookPoint);
        if (!previousAttack)
        {
            RaycastHit hitinfo;
            if (Physics.Raycast(attackingRaycastArea.transform.position, attackingRaycastArea.transform.forward,
                out hitinfo, attackingRadius))
            {
                Player playerbody = hitinfo.transform.GetComponent<Player>();
                if (playerbody != null)
                {
                    playerbody.PlayerHitDamage(giveDamage);
                }
            }
            previousAttack = true;
            Invoke(nameof(ActiveAttacking), timeAttack);
        }
    }
    private void ActiveAttacking()
    {
        previousAttack = false;
    }
    public void ZombieHitDame(float takeDamage)
    {
        Pursueplayer();
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);
        if (presentHealth <= 0)
        {
            ZombieDie();
        }

    }
    private void ZombieDie()
    {
        col.enabled = false;
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0;
        attackingRadius = 0;
        visionRadius = 0;
        playerInattackingRadius = false;
        playerInvisionRadius = false;
        Destroy(gameObject, 5f);
        anim.SetBool("die", true);
        anim.SetBool("walking", false);
        anim.SetBool("run", false);
    }
}
