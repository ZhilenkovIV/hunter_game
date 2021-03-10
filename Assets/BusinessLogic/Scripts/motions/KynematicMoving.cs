using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KynematicMoving : MonoBehaviour, IMotion
{
    private Rigidbody2D rb;
    private Vector2 speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Execute()
    {
        rb.velocity = speed;
    }

    public void SetSpeed(Vector2 speed)
    {
        this.speed = speed;
    }

    public void SetSpeedX(float speedX)
    {
        speed.x = speedX;
    }

    public void SetSpeedY(float speedY)
    {
        speed.x = speedY;
    }

    public void Undo()
    {
        rb.velocity = Vector2.zero;
    }

    public void Suspend(){

    }
    
    public void Resume(){

    }

    public void Suspend(float time){
        
    }

}
