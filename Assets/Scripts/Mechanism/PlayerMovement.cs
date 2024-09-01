using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Joystick Movejoystick;
    public Button fireButton;
    public float rotationSpeed = 360f; // Degrees per second

    public Transform spwanPoint;
    public GameObject bulletPrefab;
    public float bulletForce = 5f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public bool isFiring = false;

    public bool playerIsDead = false;
    public int playerHealth = 25;

    public ParticleSystem deathParticle;
    public ParticleSystem fireParticle;


    public void Start()
    {
        fireButton.onClick.AddListener(handleFire);
    }

    public void Update()
    {
        if (playerHealth < 0) 
        {
            if(!playerIsDead)
            {
                Debug.Log("player is dead");
                playerIsDead = true;
                Die();
            }
          
        }

        if(!playerIsDead)
        {
            movementInput();
            handleFire();
        }
    }


    public void movementInput()
    {
      
        float horizontalMovement = Movejoystick.Horizontal;
        float verticalMovement = Movejoystick.Vertical;

        if (horizontalMovement != 0)
        {
            Quaternion targetRotation = Quaternion.Euler(0, horizontalMovement * rotationSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation * transform.rotation, rotationSpeed * Time.deltaTime);
        }

        if (verticalMovement != 0)
        {
            Vector3 movement = transform.forward * verticalMovement * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

    }

    public void startFire()
    {
        isFiring = true;
    }

    public void stopFire()
    {
        isFiring = false;
    }


    public void handleFire()
    {
        if (isFiring && Time.time >= nextFireTime)
        {
            fireMechanism();
            nextFireTime = Time.time + fireRate; // Update the next fire time
        }
    }
    public void fireMechanism()
    {
        GameObject bullet = Instantiate(bulletPrefab, spwanPoint.position, spwanPoint.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            fireParticle.Play();
            bulletRb.AddForce(spwanPoint.forward * bulletForce, ForceMode.Impulse);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            playerHealth -= 5;
            Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        if (deathParticle != null)
        {
            ParticleSystem deathEffect = Instantiate(deathParticle, transform.position, Quaternion.identity);
            deathEffect.Play();

            Destroy(deathEffect.gameObject, deathEffect.main.duration);
        }


        StartCoroutine(restartLevel());
        
    }


    IEnumerator restartLevel()
    {
        Debug.Log("Waiting for 10 seconds");
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("GameScene");
    }
}

