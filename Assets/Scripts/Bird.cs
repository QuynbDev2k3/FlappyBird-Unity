using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _forceMoveUp = 12;
    public bool gameStart;
    public Action onPassedPipe;
    public Action onHitObstacle;

    private void Awake()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        gameStart = false;
        _rigidbody.gravityScale = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameStart == true)
            {
                _rigidbody.gravityScale = 8;
            }
            // SoundController._instance.BirdMoveUp("wing", 0.5f);
            BirdMoveUp();
        }   
    }

    private void BirdMoveUp()
    {
        SoundController._instance.BirdMoveUp("wing", 0.5f);
        _rigidbody.linearVelocity = Vector2.up * _forceMoveUp;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        onPassedPipe?.Invoke();
        SoundController._instance.BirdMoveUp("point", 0.5f);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        onHitObstacle?.Invoke();
        _rigidbody.gravityScale = 0;
        SoundController._instance.BirdMoveUp("die", 0.5f);
    }
}
