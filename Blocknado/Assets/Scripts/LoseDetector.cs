using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseDetector : MonoBehaviour
{
    [SerializeField] private string sceneName = "GameOver";
    [SerializeField] private GameObject gameOverScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(sceneName);
        //GameStatus gameStatus = FindObjectOfType<GameStatus>();
        //gameOverScreen.SetActive(true);
        //gameStatus.Score();
    }
}
