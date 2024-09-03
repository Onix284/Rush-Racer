using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8.5f;
    public Joystick Movejoystick;
    public Button fireButton;
    public float rotationSpeed = 90f; // Degrees per second

    public Transform spwanPoint;
    public GameObject bulletPrefab;
    public float bulletForce = 150f;
    public float fireRate = 0.155f;
    private float nextFireTime = 0f;
    public bool isFiring = false;

    public bool playerIsDead = false;
    public float playerHealth = 25;

    public ParticleSystem deathParticle;
    public ParticleSystem fireParticle;

    public Image healthBar;
    private float maxHealth = 100f;
    
    public PlayerSounds playerSounds;
    
    public void Start()
    {
        fireButton.onClick.AddListener(handleFire);
        maxHealth = playerHealth;
    }

    public void Update()
    {
        if (playerHealth <= 0) 
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

        bool isMoving = horizontalMovement != 0 || verticalMovement != 0;

        if (isMoving)
        {

           // playerSounds.movement();
        }
        else
        {
            // playerSounds.StopMovementSound();
        }

        if (horizontalMovement != 0)
        {
            Quaternion targetRotation = Quaternion.Euler(0, horizontalMovement * rotationSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation * transform.rotation, rotationSpeed * Time.deltaTime);
        }

        if (verticalMovement != 0)
        {
            playerSounds.movement();
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
            playerSounds.fire();
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
            //playerSounds.fire();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            playerHealth -= 5;
            healthBar.fillAmount = Mathf.Clamp01(playerHealth / maxHealth);
            Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        if (deathParticle != null)
        {
            ParticleSystem deathEffect = Instantiate(deathParticle, transform.position, Quaternion.identity);
            deathEffect.Play();
            playerSounds.death();
            healthBar.fillAmount = 0f;
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

