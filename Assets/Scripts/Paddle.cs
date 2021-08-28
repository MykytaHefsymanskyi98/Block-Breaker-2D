using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] public float unitsAmount;
    [SerializeField] public float xPosMin;
    [SerializeField] public float xPosMax;
    [SerializeField] Ball ballObject;
    Game game;

    private void Start()
    {
            game = FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update()
    {
            if(!game.GetOnPause())
            {
            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
            paddlePos.x = Mathf.Clamp(GetXPosition(), xPosMin, xPosMax);
            transform.position = paddlePos;
            }           
    }

    float GetXPosition()
    {
        if(game.AutoPlayEnable())
        {
            return ballObject.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * unitsAmount;
        }
    }
}
