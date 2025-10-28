using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    public static ScoreScript Instance;
    public int score = 0;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }

    public void AddScore(int pts)
    {
        score += pts;
        UpdateScore();
    }

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
