using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //configurat param
    [SerializeField] public Paddle paddle1;
    [SerializeField] public float launchByX = 0.0f;
    [SerializeField] public float launchByY = 15.0f;
    [SerializeField] float randomFactor = 0.8f;
    [SerializeField] AudioClip[] ballAudio;

    //states
    Vector2 ballToPaddleVector;
    bool hasStarted = false;
    float velocityDelta = 3f;

    //cached component references
    Rigidbody2D ballRigidBody;
    AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        ballToPaddleVector = transform.position - paddle1.transform.position;
        ballRigidBody = GetComponent<Rigidbody2D>();
        myAudio = GetComponent<AudioSource>();
        ballRigidBody.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        BallVelocityControl();
        StartBallPositionProcess();
    }

    void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + ballToPaddleVector;

    }

    void LaunchBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            ballRigidBody.simulated = true;
            ballRigidBody.velocity = new Vector2(launchByX, launchByY);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 randomPath = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        if (hasStarted == true)
        {
            AudioClip clip = ballAudio[UnityEngine.Random.Range(0, ballAudio.Length)];
            myAudio.PlayOneShot(clip);
            if(collision.gameObject.tag == "Left Wall")
            {
                ballRigidBody.velocity += randomPath;
            }
            else if(collision.gameObject.tag == "Top Wall")
            {
                ballRigidBody.velocity -= randomPath;
            }
            else if(collision.gameObject.tag == "Right Wall")
            {
                ballRigidBody.velocity += randomPath;
            }
        }
    }

    void BallVelocityControl()
    {
        if (ballRigidBody.velocity.y > launchByY)
        {
            ballRigidBody.velocity -= new Vector2(ballRigidBody.velocity.x / velocityDelta, ballRigidBody.velocity.y / velocityDelta);
        }
    }

    void StartBallPositionProcess()
    {
        if (hasStarted == false)
        {
            LockBallToPaddle();

            LaunchBall();
        }
    }
}
