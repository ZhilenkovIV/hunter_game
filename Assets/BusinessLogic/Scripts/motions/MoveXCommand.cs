using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveXCommand : MonoBehaviour, IMotion
{
    private Rigidbody2D rb;

    //[HideInInspector]
    public float speed;

    //public float BaseAttack;

    private bool canMove = true;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Execute()
    {
        if(canMove){
            rb.velocity = new Vector2(speed, rb.velocity.y);
            //rb.AddForce(new Vector2(speed, rb.velocity.y) / rb.mass, ForceMode2D.Impulse);
            if (Mathf.Abs(speed) > 0.01) {
                Vector3 theScale = rb.transform.localScale;
                //зеркально отражаем персонажа по оси Х
                theScale.x = Mathf.Abs(theScale.x) * Mathf.Sign(speed);
                //задаем новый размер персонажа, равный старому, но зеркально отраженный
                rb.transform.localScale = theScale;
            }
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

    public void SetSpeed(Vector2 speed)
    {
        this.speed = speed.x;
    }

    public void SetSpeedX(float speedX)
    {
        speed = speedX;
    }

    public void SetSpeedY(float speedY)
    {
        
    }

    public void Suspend(){
        canMove = false;
    }

    public void Resume(){
        canMove = true;
    }

    private IEnumerator SuspendCoroutine(float time){
        Suspend();
        for(float t = 0; t < time; t += Time.deltaTime){
            yield return null;
        }
        Resume();
    }

    public void Suspend(float time){
        StartCoroutine(SuspendCoroutine(time));
    }
}
