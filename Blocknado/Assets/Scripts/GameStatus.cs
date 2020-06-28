using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 10f)] public float gameSpeed = 1f;
    [SerializeField] private int currentScore;
    [SerializeField] private int pointsPerBlock;

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        currentScore = 0;
        Score();
    }

    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlock;
        Score();
    }

    public void Score()
    {
        scoreText.text = currentScore.ToString();
    }
}
