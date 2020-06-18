using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle paddle;

    private Vector2 paddleBallVector; // distance between center of paddle and ball

    private void Start()
    {
        SetStartingPos();
        paddleBallVector = transform.position - paddle.transform.position;
    }

    private void Update()
    {
        StickBallToPaddle();
    }

    private void SetStartingPos()
    {
        transform.position = new Vector2(paddle.transform.position.x, paddle.transform.position.y + 0.45f);
    }

    private void StickBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleBallVector;
    }
}
