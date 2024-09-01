using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour
{
    public GameObject pauseCamera;
    public GameObject Maincamera;
    public Canvas mainCanvas;
    public Button pausbutton;
    public Button resumeButton;

    public Button restartButton;
    public Button quitButton;

    public Button musicButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Image musicIconImage;

    public bool isSoundPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        pausbutton.onClick.AddListener(pausMenu);
        resumeButton.onClick.AddListener(resumeGame);
        restartButton.onClick.AddListener(restartGame);
        quitButton.onClick.AddListener(quitGame);

        musicButton.onClick.AddListener(ToggleMusic);

        Maincamera.SetActive(true);
        mainCanvas.gameObject.SetActive(true);
        pauseCamera.SetActive(false);

        UpdateMusicState();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSoundPlaying) 
        {
           
        }
    }

    void pausMenu()
    {
        Debug.Log("Game is paused");
        
        Maincamera.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        pauseCamera.SetActive(true);
        Time.timeScale = 0f;
    }

    void resumeGame()
    {
        Debug.Log("Game is resumed");
        Maincamera.SetActive(true);
        mainCanvas.gameObject.SetActive(true);
        pauseCamera.SetActive(false);
        Time.timeScale = 1f;
    }

    void restartGame() 
    {
        Debug.Log("Game is restarting");
        StartCoroutine(restartLevel());
    }

    private IEnumerator restartLevel()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Attempting to reload scene: " + sceneName);
    }

    void StopMusic()
    {
        isSoundPlaying = false;
        Debug.Log("Music is stopped");
    }

    private void ToggleMusic()
    {
        isSoundPlaying = !isSoundPlaying; // Toggle the sound playing state
        UpdateMusicState(); // Update music and button state
    }

    private void UpdateMusicState()
    {
        //if (backgroundMusic != null)
        //{
            if (isSoundPlaying)
            {
            //backgroundMusic.Play();
                
                musicIconImage.sprite = musicOnSprite;
                Debug.Log("Music is playing");  
            }
            else
            {
            //backgroundMusic.Stop();
                musicIconImage.sprite = musicOffSprite;
            Debug.Log("Music is stopped");
            }
      }

    void quitGame()
    {
        Application.Quit();
    }
}
