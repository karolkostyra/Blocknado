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
        int ballNumber = FindObjectsOfType<Ball>().Length;

        if(ballNumber == 1)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Destroy(collision.gameObject);
        }
        //GameStatus gameStatus = FindObjectOfType<GameStatus>();
        //gameOverScreen.SetActive(true);
        //gameStatus.Score();
    }
}
