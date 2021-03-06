﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public delegate void BlocksDestroyedAction();
    public static event BlocksDestroyedAction OnBlocksDestroyed;

    [SerializeField] private int numberOfBlocks;

    private SceneLoader sceneLoader;


    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
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
            OnBlocksDestroyed();
        }
    }
}