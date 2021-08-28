using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //configurat param
    [SerializeField] AudioClip clipSound;
    [SerializeField] GameObject particalEffect;
    [SerializeField] Sprite[] hitSprites;
    int maxHits;

    //references
    Level count;
    Game gameScore;

    int hitCount = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "BreakableBlock")
        {
            CountigHits();
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {

        maxHits = hitSprites.Length + 1;
        gameScore = FindObjectOfType<Game>();
        count = FindObjectOfType<Level>();
        if (tag == "BreakableBlock")
        {
            count.CountBlocks();
        }
    }

    void DestroyOneBlock()
    {
        Destroy(gameObject);
        count.DestroyBlock();
        gameScore.Score();
        PlayAudioBlock();
        ShowParticalEffects();
    }

    void ShowParticalEffects()
    {
        GameObject sparkles = Instantiate(particalEffect, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

    void PlayAudioBlock()
    {
        AudioSource.PlayClipAtPoint(clipSound, Camera.main.transform.position);
    }

    void CountigHits()
    {
        hitCount++;
        if(maxHits <= hitCount)
        {
            DestroyOneBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    void ShowNextHitSprite()
    {
        int spriteIndex = hitCount - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing" + gameObject.name);
        }
    }
}
