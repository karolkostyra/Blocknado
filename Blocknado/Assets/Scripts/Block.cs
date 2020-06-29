using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private Sprite[] hitSprites;
    [SerializeField] private bool multiplyBalls;
    private int timesHit;

    private Level level;
    private GameSession gameStatus;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameSession>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
            MultiplyBalls();
        }
    }

    private void MultiplyBalls()
    {
        if (multiplyBalls)
        {
            Ball ballReference = FindObjectOfType<Ball>();
            for (int i = 0; i < 2; i++)
            {
                GameObject ball = Instantiate
                        (ballReference.gameObject,
                        gameObject.transform.position,
                        gameObject.transform.rotation);

                ball.GetComponent<Rigidbody2D>().velocity += new Vector2
                                (Random.Range(5, 10), Random.Range(5, 10));
            }
        }
    }

    private void HandleHit()
    {
        int maxHits = hitSprites.Length + 1;
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            NextHitSprite();
        }
    }

    private void NextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array!");
        }
    }

    private void DestroyBlock()
    {
        PlayDestroySFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
        gameStatus.AddToScore();
    }

    private void PlayDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
