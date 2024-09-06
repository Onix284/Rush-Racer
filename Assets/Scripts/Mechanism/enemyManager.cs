using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public int enemiesToKill = 8; // Number of enemies required to complete the level
    private int enemiesKilled = 0;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of EnemyManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        Debug.Log("Enemies Killed: " + enemiesKilled);

        if (enemiesKilled >= enemiesToKill)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Level Completed!");
        SceneManager.LoadScene("LevelCompleted");
    }
}
