using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionHandler : MonoBehaviour
{
   // public float restartDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("environment"))
        {
            //GetComponent<PlayerMovement>().enabled = false;
            //Invoke("RestartLevel", restartDelay);
            Debug.Log("Player collided to environment");
        }
    }

    //private void RestartLevel()
    //{
    //    SceneManager.LoadScene("SampleScene");
    //}
}
