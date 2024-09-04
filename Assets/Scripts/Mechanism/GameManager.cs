using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas tapToStart;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        tapToStart.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Time.timeScale = 1f;
                tapToStart.enabled = false;
            }
        }
    }
}
