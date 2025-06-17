using UnityEngine;

public class Pipe : MonoBehaviour
{
    private float speed = 3;
    
    // Update is called once per frame
    void Update() // Được gọi dựa trên FPS: Time.DeltaTime = 1 / FPS;
    {
        Move();
    }
    
    private void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
