using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    public int enemyHealth = 20;
    public Transform player;

    public GameObject bulletPrefab;
    public float fireRange = 15f;
    public float fireRate = 0.75f;
    public Transform firePoint;
    public float bulletforce = 20f;

    private NavMeshAgent agent;
    private float fireTimer;

    private PlayerMovement pm;
    private bool isFiring = true;

    public ParticleSystem hitParticle;
    public ParticleSystem deathParticle;

    public EnemySoundManager soundManager;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fireTimer = fireRate;

        pm = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (pm != null && pm.playerIsDead)
        {
            StopFiring();
            return;
        }

        if (enemyHealth <= 0)
        {
            agent.velocity = Vector3.zero;
            StartCoroutine(enemyDeath());
            return;
        }


        agent.destination = player.position;
        transform.LookAt(player.position);

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate && IsPlayerInSight())
        {
            FireBullet();
            soundManager.fire();
            fireTimer = 0f;
        }
    }

    bool IsPlayerInSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        if (directionToPlayer.magnitude < fireRange)
        {
            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hit;

            Debug.DrawLine(transform.position, directionToPlayer, Color.red);

            if (Physics.Raycast(ray, out hit, fireRange))
            {
                if (hit.transform.tag == "player")
                {
                    return true;
                }
            }
        }
        return false;
    }

    void FireBullet()
    {

        if (bulletPrefab != null && firePoint != null)
        {

            GameObject enemyBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Vector3 directionToPlayer = (player.position - firePoint.position).normalized;

            enemyBullet.GetComponent<Rigidbody>().velocity = directionToPlayer * bulletforce;
        }
    }

    private void StopFiring()
    {
        isFiring = false; // Disable firing
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            enemyHealth -= 5;
            hitParticle.Play();
            soundManager.death();
            Destroy(collision.gameObject);
            Debug.Log("Enemy health : " + enemyHealth);
        }
    }


    IEnumerator enemyDeath()
    {
        deathParticle.Play();
        yield return new WaitForSeconds(deathParticle.main.duration);
        //Destroy(gameObject);
    }
}

