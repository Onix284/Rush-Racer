using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(playGame);
        exitButton.onClick.AddListener(exitGame);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void playGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void exitGame()
    {
        Debug.Log("Quitting app");
        Application.Quit();
    }

}
