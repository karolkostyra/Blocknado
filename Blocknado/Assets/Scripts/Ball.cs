using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle paddle;
    [SerializeField] private float xPush;
    [SerializeField] private float yPush;
    [SerializeField] private float randomFactor = 0.5f;
    [SerializeField] private AudioClip[] ballSounds;

    private AudioSource ballAudioSource;
    private Rigidbody2D rigidbody2D;

    private Vector2 paddleBallVector; // distance between center of paddle and ball
    private Vector3 lastBallPos;
    [SerializeField] private bool isLaunched;

    private Vector3 mouseDelta = Vector3.zero;
    private Vector3 lastMousePosition = Vector3.zero;

    private void Start()
    {
        isLaunched = false;
        SetStartingPos();
        UpdateLastBallPos();
        paddleBallVector = transform.position - paddle.transform.position;
        ballAudioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mouseDelta = Input.mousePosition - lastMousePosition;
        lastMousePosition = Input.mousePosition;
        //Debug.Log("SPEED: " + mouseDelta);

        DetectMouseDirection();
        if (!isLaunched)
        {
            StickToPaddle();
            LaunchOnMouseClick();
        }
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
            //Debug.Log("left");
            xPush = 1f * (mouseDelta.x/5f);
            UpdateLastBallPos();
        }
        else if(lastBallPos.x < transform.position.x)
        {
            //Debug.Log("right");
            xPush = 1f * (mouseDelta.x / 5f);
            UpdateLastBallPos();
        }
        else
        {
            //Debug.Log("up");
            xPush = 0f;
            UpdateLastBallPos();
        }
    }

    private void UpdateLastBallPos()
    {
        lastBallPos = transform.position;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isLaunched = true;
            rigidbody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0, randomFactor),
                                            Random.Range(0, randomFactor));
        if (isLaunched)
        {
            AudioClip randomClip = ballSounds[Random.Range(0, ballSounds.Length)];
            ballAudioSource.PlayOneShot(randomClip);
            rigidbody2D.velocity += velocityTweak;
        }
    }
}
