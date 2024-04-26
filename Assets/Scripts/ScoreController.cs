using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    public TMP_Text timerText; // Reference to UI text to display the timer
    public GameObject gameOverPanel; // Reference to the game over panel
    public TMP_Text scoreText; // Reference to the UI text to display the score
    public TMP_Text resultText; // Reference to the UI text to display the score
    private float remainingTime; // Remaining time

    private bool isGameOver = false;
    private bool isPaused = false;
    private int score = 0;

    void Start()
    {
        remainingTime = totalTime;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isGameOver && !isPaused)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0f)
            {
                remainingTime = 0f;
                GameOver();
            }
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = string.Format("Time {0:00}:{1:00}", minutes, seconds);
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; //Pause the game
        gameOverPanel.SetActive(true);

        // You can add additional game over logic here
        resultText.text = "Game Over \n You Score: " + score;
    }

    // Method to increment score
    public void IncrementScore(int points)
    {
        score += points;
    }

    public void DecrementScore(int points)
    {
        score -= points;
    }

    //Method to resume the game
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
}
