using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle paddle;

    private Vector2 paddleBallVector; // distance between center of paddle and ball
    private Vector3 lastBallPos;

    private void Start()
    {
        SetStartingPos();
        UpdateLastBallPos();
        paddleBallVector = transform.position - paddle.transform.position;
    }

    private void Update()
    {
        StickToPaddle();
        DetectMouseDirection();
    }

    private void SetStartingPos()
    {
        transform.position = new Vector2(paddle.transform.position.x, paddle.transform.position.y + 0.45f);
    }

    private void StickToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleBallVector;
    }

    private void DetectMouseDirection()
    {
        if(lastBallPos.x > transform.position.x)
        {
            Debug.Log("left");
            UpdateLastBallPos();
        }
        else if(lastBallPos.x < transform.position.x)
        {
            Debug.Log("right");
            UpdateLastBallPos();
        }
        else
        {
            Debug.Log("up");
            UpdateLastBallPos();
        }
    }

    private void UpdateLastBallPos()
    {
        lastBallPos = transform.position;
    }
}
