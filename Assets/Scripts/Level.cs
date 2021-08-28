using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    //configurat param

    //cached component references
    SceneLoader loader;
    
    //states
    int blockCount;

    // Start is called before the first frame update
    void Start()
    {
        loader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
            blockCount++;
    }

    public void DestroyBlock()
    {
        blockCount--;
        if (blockCount <= 0)
        {
            loader.LoadNextScene();
        }
    }

   
}
