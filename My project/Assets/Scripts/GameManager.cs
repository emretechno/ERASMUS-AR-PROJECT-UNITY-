using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int totalEnemies = 0; // Initial number of enemies in the game
    public Chronometer chronometer; // Reference to the Chronometer script

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEnemy()
    {
        totalEnemies++;
        Debug.Log("Enemy added. Total enemies: " + totalEnemies);
    }

    public void EnemyKilled()
    {
        totalEnemies--;
        Debug.Log("Enemy killed. Remaining enemies: " + totalEnemies);
        if (totalEnemies <= 0)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        // Log message to confirm method is called
        Debug.Log("All enemies are dead. Finishing game...");

        // Stop the chronometer
        if (chronometer != null)
        {
            chronometer.StopTimer();
        }

        // Implement any other game finish logic here
    }
}
