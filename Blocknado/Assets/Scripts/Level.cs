using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int numberOfBlocks;
    [SerializeField] private GameObject levelSummary;

    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        levelSummary.SetActive(false);
    }

    public void CountBlocks()
    {
        numberOfBlocks++;
    }

    public void BlockDestroyed()
    {
        numberOfBlocks--;
        if(numberOfBlocks <= 0)
        {
            levelSummary.SetActive(true);
            GameStatus gameStatus = FindObjectOfType<GameStatus>();
            gameStatus.gameSpeed = 0f;
            //sceneLoader.LoadNextScene();
        }
    }
}