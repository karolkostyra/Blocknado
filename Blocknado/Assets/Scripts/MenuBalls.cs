using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBalls : MonoBehaviour
{
    [SerializeField] private float xPush;
    [SerializeField] private float yPush;
    [SerializeField] private float minSpeed = 10f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float directionModifier = 2f;
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
        UpdateLastBallPos();
        ballAudioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        mouseDelta = Input.mousePosition - lastMousePosition;
        lastMousePosition = Input.mousePosition;
        //Debug.Log("SPEED: " + mouseDelta);
        DetectMouseDirection();
        Launch(isLaunched);
        LimitBallSpeed();
    }

    private void LimitBallSpeed()
    {
        if (rigidbody2D.velocity.magnitude < minSpeed)
            rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, minSpeed);

        if (rigidbody2D.velocity.magnitude > maxSpeed)
            rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maxSpeed);
    }

    private void DetectMouseDirection()
    {
        if (lastBallPos.x > transform.position.x)
        {
            //Debug.Log("left");
            xPush = 1f * (mouseDelta.x / 5f);
            UpdateLastBallPos();
        }
        else if (lastBallPos.x < transform.position.x)
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

    private void DetectBallDirection()
    {
        var sign = Random.Range(0, 2); //specifies the sign of directionModifier(- or +)

        if (Mathf.Abs(lastBallPos.x - gameObject.transform.position.x) < 0.1f)
        {
            AddImpulse(sign, transform.right);
        }
        if (Mathf.Abs(lastBallPos.y - gameObject.transform.position.y) < 0.1f)
        {
            AddImpulse(sign, transform.up);
        }
    }

    private void AddImpulse(int x, Vector3 direction)
    {
        if (x == 0)
        {
            rigidbody2D.AddForce(direction * -directionModifier, ForceMode2D.Impulse);
        }
        else
        {
            rigidbody2D.AddForce(direction * directionModifier, ForceMode2D.Impulse);
        }
    }

    private void UpdateLastBallPos()
    {
        lastBallPos = transform.position;
    }

    private void Launch(bool flag)
    {
        if (!flag)
        {
            isLaunched = true;
            rigidbody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLaunched && collision.gameObject.tag != "Ball")
        {
            //AudioClip randomClip = ballSounds[Random.Range(0, ballSounds.Length)];
            //ballAudioSource.PlayOneShot(randomClip);
            DetectBallDirection();
        }
        
    }
}