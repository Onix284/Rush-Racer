using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Joystick Movejoystick;
    public Button fireButton;
    public Transform spwanPoint;
    public GameObject bulletPrefab;
    public float bulletForce = 5f;
    public float fireRate = 0.5f; 
    private float nextFireTime = 0f;
    public bool isFiring = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start method called");
        fireButton.onClick.RemoveAllListeners();
        fireButton.onClick.AddListener(handleFireTouch);
    }

    // Update is called once per frame
    void Update()
    {

        movementInput();
    }

    void movementInput()
    {
        float horizontalMovement = Movejoystick.Horizontal;
        float verticalMovement = Movejoystick.Vertical;

        Vector3 movemnt = new Vector3(horizontalMovement, 0, verticalMovement).normalized;

        transform.Translate(movemnt * moveSpeed * Time.deltaTime, Space.World);

        if (movemnt != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(movemnt, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 360 * Time.deltaTime);
        }
    }

    void handleFireTouch()
    {
        if (Time.time >= nextFireTime)
            {
                fireMechanism();
                nextFireTime = Time.time + fireRate; // Update the next fire time
            }
    }

    void fireMechanism()
    {

            Debug.Log("Fire Button Clicked!");
            GameObject bullet = Instantiate(bulletPrefab, spwanPoint.position, spwanPoint.rotation);

            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            if (bulletRb != null)
            {
                bulletRb.AddForce(spwanPoint.forward * bulletForce, ForceMode.Impulse);
            }
    }
}
