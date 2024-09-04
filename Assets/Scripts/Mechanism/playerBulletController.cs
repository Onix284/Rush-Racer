using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBulletController : MonoBehaviour
{
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
        if(collision.gameObject.CompareTag("environment"))
        {
            Destroy(this.gameObject);
        }

        if(collision.gameObject.CompareTag("heal"))
        {
            Destroy(this.gameObject);
        }
    }

    
}
