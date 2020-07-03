using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 10f)] public float gameSpeed = 1f;
    [SerializeField] private int currentScore;
    [SerializeField] private int pointsPerBlock;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private bool isAutoPlayEnabled;
    private Transform levelSummaryObj;

    private void Awake()
    {
        levelSummaryObj = gameObject.transform.Find("GameCanvas").transform.Find("LevelSummary");
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
    }

    void Update()
    {
        Time.timeScale = gameSpeed;
        RestartLevel();
    }

    private static void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlock;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void LevelSummary()
    {
        gameSpeed = 0f;
        levelSummaryObj.gameObject.SetActive(true);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelSummaryObj.gameObject.SetActive(false);
        gameSpeed = 1f;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Level.OnBlocksDestroyed += LevelSummary;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Level.OnBlocksDestroyed -= LevelSummary;
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
