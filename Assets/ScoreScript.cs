using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    public static ScoreScript Instance; // instance for scorescript
    public int score = 0; // score variable
    public TMP_Text scoreText; // text for the score
    public TMP_Text highScoreText; // text for high score

    // awake
    private void Awake()
    {
        // checks for null instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start updates score at the beginning
    void Start()
    {
        UpdateScore();
    }

    // adds points to total score
    public void AddScore(int pts)
    {
        score += pts;
        UpdateScore();
    }

    // updates the score to the text and checks high score
    void UpdateScore()
    {
        if (scoreText != null) 
        {
            scoreText.text = "Score: " + score;
        }
        if (highScoreText != null) 
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            if (score > highScore)
            {
                highScore = score;
            }
            highScoreText.text = "High Score: " + highScore;

        }
    }

    // game over saves high score and resets scene
    public void GameOver()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
